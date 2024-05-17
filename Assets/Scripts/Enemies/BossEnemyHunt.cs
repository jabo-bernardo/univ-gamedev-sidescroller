using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Enemy))]
public class BossEnemyHunt : EnemyHuntBase
{
    private BossEnemyAttack bossEnemyAttack;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Player");
        bossEnemyAttack = GetComponent<BossEnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        distanceBetweenTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceBetweenTarget <= huntRange && !bossEnemyAttack.IsAboutToDash()) enemy.SetShouldHunt(true);
        else enemy.SetShouldHunt(false);

        // bool IS_FACING_PLAYER = (enemy.GetDirection() < 0 && target.transform.position.x - transform.position.x < 0) || (enemy.GetDirection() > 0 && target.transform.position.x - transform.position.x > 0);

        // if (!IS_FACING_PLAYER) enemy.SetShouldHunt(true);

        if (enemy.GetShouldHunt()) {
            float directionBasis = target.transform.position.x - transform.position.x;
            enemy.SetDirection(directionBasis > 0 ? 1 : -1);
            if (enemy.GetDirection() < 0) transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            else if (enemy.GetDirection() > 0) transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (enemy.GetIsAttacking()) {
            enemy.animator.SetBool("isWalking", false);
        }

        if (enemy.GetShouldHunt() && !enemy.GetIsAttacking()) {
            rigidBody.velocity = new Vector2(enemy.GetDirection() * movementSpeed, rigidBody.velocity.y);
            enemy.animator.SetBool("isWalking", true);
        }

    }
}
