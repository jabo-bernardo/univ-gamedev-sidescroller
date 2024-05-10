using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbBandit : MonoBehaviour
{
    /**
     * Characteristics: Dumb, only fists. very short range.
     * will probably die before hitting the player :p
     */

    public GameObject target;
    public float rangeBeforeIPursue = 4f;
    public float attackRange = 1;
    public float movementSpeed = 2f;
    public float attackSpeed = 0.5f;
    public float damage = 10.0f;

    [Header("Hitbox")]
    public EnemyAttackHitbox hitbox;

    [Header("Effects")]
    public GameObject slashEffectObject;

    private Rigidbody2D rb;

    private bool shouldPursue = false;
    private bool isAttacking = false;

    private float direction;
    

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        hitbox.SetDamage(damage);
    }

    void Update()
    {
        float distanceBetweenTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceBetweenTarget < rangeBeforeIPursue) {
            shouldPursue = true;
        } else
        {
            shouldPursue = false;
        }

        if (distanceBetweenTarget < attackRange && !isAttacking)
        {
            StartCoroutine(InflictDamage());
        }

        if (shouldPursue && !isAttacking)
        {
            float howFarAmI = target.transform.position.x - transform.position.x;
            direction = howFarAmI > 0 ? 1 : -1;
            if (direction < 0)
		    {
			    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
		    } else if (direction > 0) { 
			    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
		    }
            rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
        }
    }

    IEnumerator InflictDamage()
    {
        isAttacking = true;

        GameObject slashEffect = Instantiate(slashEffectObject, transform);
        slashEffect.transform.SetLocalPositionAndRotation(new Vector2(0.25f, -0.25f), transform.rotation);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bool IS_PLAYER_IN_BEFORE_ATTACK_DISTANCE = Vector2.Distance(player.transform.position, gameObject.transform.position) < attackRange;

        yield return new WaitForSeconds(attackSpeed / 2);
        bool IS_PLAYER_IN_ATTACK_DISTANCE = Vector2.Distance(player.transform.position, gameObject.transform.position) < attackRange;
        bool IS_FACING_PLAYER = (direction < 0 && player.transform.position.x - transform.position.x < 0) || (direction > 0 && player.transform.position.x - transform.position.x > 0);
        if (IS_PLAYER_IN_ATTACK_DISTANCE && IS_FACING_PLAYER) {
            player.GetComponent<Player>().TakeDamage(damage);
        }

        if (IS_PLAYER_IN_BEFORE_ATTACK_DISTANCE && !(IS_PLAYER_IN_ATTACK_DISTANCE && IS_FACING_PLAYER)) {
            Debug.Log("Dodged Btich!");
        }

        yield return new WaitForSeconds(attackSpeed / 2);
        isAttacking = false;
    }
}
