using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100.0f;

    private bool shouldHunt = false;
    private bool isAttacking = false;
    private float direction = 1;

    public GameObject bloodParticleSystem;
    private Rigidbody2D rb;
    public Animator animator;
    private AudioSource audioSource;

    [Header("Rewards")]
    public GameObject pearl;
    public int pearlCount = 3;

    [Header("Sound")]
    public  AudioClip[] hitSound;

    [Header("Goblin Specific")]
    public DialogueTrigger killDialogue;
    private bool isDying;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        pearlCount = (int) Mathf.Round(health / 35);
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (health <= 0) {
            if (isDying) return;
            isDying = true;
            if (GameManager.Instance.killTracker.ContainsKey(gameObject.name)) {
                GameManager.Instance.killTracker[gameObject.name] = GameManager.Instance.killTracker[gameObject.name] + 1;
            } else {
                GameManager.Instance.killTracker[gameObject.name] = 1;
            }
            StartCoroutine(KillMeAlready());           
        }
    }

    IEnumerator KillMeAlready() {
        if (isDying) yield return null;
        isDying = true;

        bool SHOULD_DROP_PEARL = Random.Range(0, 10) <= 5;
        if (pearlCount > 0 && pearl && SHOULD_DROP_PEARL) {
            for (int x = 0; x < pearlCount; x++) {
                Instantiate(pearl, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
        if (gameObject.name.Contains("Spider")) {
            if (GameManager.Instance.GetIsFirstGoblinKill()) {
                StartCoroutine(FirstGoblinKillDialogue());
            }
        }
        if (GameManager.Instance.yoloTracker.ContainsKey(gameObject.name))
            yield return null;
        GameManager.Instance.yoloTracker.Add(gameObject.name, true);
        yield return null;
    }

    IEnumerator FirstGoblinKillDialogue() {
        killDialogue.TriggerDialogue();
        GameManager.Instance.IHaveKilledAGoblin();
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("AttackHit")) {
            TakeDamage(GameManager.Instance.GetPlayerBaseDamage(), collider.gameObject);
        }
        if (collider.gameObject.CompareTag("AttackHitMace")) {
            TakeDamage(GameManager.Instance.GetPlayerBaseDamage() * 2.0f, collider.gameObject);
        }
    }

    public Enemy TakeDamage(float damage, GameObject damageDealer = null) {
        health -= damage;
        animator.SetTrigger("isHit");
        int randomHitSound = Random.Range(0, hitSound.Length);
        if (hitSound[randomHitSound])
            audioSource.PlayOneShot(hitSound[randomHitSound]);
        if (damageDealer) {
            Instantiate(bloodParticleSystem, transform.position, Quaternion.identity);
            rb.AddForce(new Vector2((transform.position.x - damageDealer.transform.position.x) * 180f, 128f));
        }
        return this;
    }

    public bool GetShouldHunt() {
        return shouldHunt;
    }

    public bool GetIsAttacking() {
        return isAttacking;
    }
    
    public float GetDirection() {
        return direction;
    }

    public Enemy SetShouldHunt(bool _shouldHunt) {
        shouldHunt = _shouldHunt;
        return this;
    }

    public Enemy SetIsAttacking(bool _isAttacking) {
        isAttacking = _isAttacking;
        return this;
    }

    public Enemy SetDirection(float _direction) {
        direction = _direction;
        return this;
    }

    public Rigidbody2D GetRigidbody2D() {
        return rb;
    }
}
