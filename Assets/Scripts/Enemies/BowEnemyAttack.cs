using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEnemyAttack : MonoBehaviour
{
    [Header("Attack Configuration")]
    public float attackSpeed = 0.5f;
    public float attackRange = 1f;
    public float damage = 10f;
    
    [Header("Bullet")]
    public GameObject bullet;
    public GameObject launcher;

    private Enemy enemy;
    private EnemyHuntBase enemyHunt;

    void Start() {
        enemy = GetComponent<Enemy>();
        enemyHunt = GetComponent<EnemyHuntBase>();
    }

    void Update() {
        bool IS_ALLOWED_TO_ATTACK = enemyHunt.GetDistanceBetweenTarget() < attackRange && !enemy.GetIsAttacking(); 
        if (IS_ALLOWED_TO_ATTACK) {
            StartCoroutine(LaunchArrow());
        }
    }

    IEnumerator LaunchArrow()
    {
        enemy.SetIsAttacking(true);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float xDirection = player.transform.position.x - transform.position.x  < 0 ? -1 : 1;

        GameObject bulletInstance = Instantiate(bullet, launcher.transform.position, Quaternion.identity);
        EnemyBullet enemyBullet = bulletInstance.GetComponent<EnemyBullet>();
        enemyBullet.direction = new Vector2(xDirection, 0.5f);
        enemyBullet.damage = damage;
        
        yield return new WaitForSeconds(attackSpeed);
        enemy.SetIsAttacking(false);
    }
}
