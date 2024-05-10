using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100.0f;

    private bool shouldHunt = false;
    private bool isAttacking = false;
    private float direction = 1;

    public GameObject bloodParticleSystem;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (health <= 0) {
            if (GameManager.Instance.killTracker.ContainsKey(gameObject.name)) {
                GameManager.Instance.killTracker[gameObject.name] = GameManager.Instance.killTracker[gameObject.name] + 1;
            } else {
                GameManager.Instance.killTracker[gameObject.name] = 1;
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("AttackHit")) {
            TakeDamage(GameManager.Instance.GetPlayerBaseDamage(), collider.gameObject);
        }
    }

    public Enemy TakeDamage(float damage, GameObject damageDealer = null) {
        health -= damage;
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
