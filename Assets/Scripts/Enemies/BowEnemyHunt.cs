using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemyHunt : EnemyHuntBase
{
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        distanceBetweenTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceBetweenTarget >= huntRange) enemy.SetShouldHunt(true);
        else enemy.SetShouldHunt(false);

        if (enemy.GetIsAttacking()) {
            enemy.animator.SetBool("isWalking", false);
        }

        if (enemy.GetShouldHunt() && !enemy.GetIsAttacking()) {
            float directionBasis = target.transform.position.x - transform.position.x;
            enemy.SetDirection(directionBasis > 0 ? 1 : -1);
            if (enemy.GetDirection() < 0) transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            else if (enemy.GetDirection() > 0) transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            rigidBody.velocity = new Vector2(enemy.GetDirection() * movementSpeed, rigidBody.velocity.y);
            enemy.animator.SetBool("isWalking", true);
        }

    }
}
