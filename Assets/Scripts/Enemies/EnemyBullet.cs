using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class EnemyBullet : MonoBehaviour
{
    public float launchForce = 1f;
    public float damage = 25;
    public float bulletDecay = 1f;
    public Vector2 direction;

    private Rigidbody2D rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.Find("Player");
        rigidbody.velocity = (player.transform.position - transform.position).normalized  * launchForce;
        StartCoroutine(SelfDestruct());
    }

    void Update()
    {
        float angle = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator SelfDestruct() {
        yield return new WaitForSeconds(bulletDecay);
        Destroy(gameObject);

    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            collider.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
