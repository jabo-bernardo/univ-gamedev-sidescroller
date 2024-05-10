using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            collider.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public EnemyAttackHitbox SetDamage(float _damage) {
        damage = _damage;
        return this;
    }
}
