using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Enemy))]
[RequireComponent(typeof (EnemyHuntBase))]
public class EnemyDefaultAttack : MonoBehaviour
{
    [Header("Attack Configuration")]
    public float attackSpeed = 0.5f;
    public float attackRange = 1f;
    public float damage = 10f;
    
    [Header("Effects")]
    public GameObject slashEffectObject;

    private Enemy enemy;
    private EnemyHuntBase enemyHunt;

    void Start() {
        enemy = GetComponent<Enemy>();
        enemyHunt = GetComponent<EnemyHuntBase>();
    }

    void Update() {
        bool IS_ALLOWED_TO_ATTACK = enemyHunt.GetDistanceBetweenTarget() < attackRange && !enemy.GetIsAttacking(); 
        if (IS_ALLOWED_TO_ATTACK) {
            StartCoroutine(InflictDamage());
        }
    }

    IEnumerator InflictDamage()
    {
        enemy.SetIsAttacking(true);

        GameObject slashEffect = Instantiate(slashEffectObject, transform);
        slashEffect.transform.SetLocalPositionAndRotation(new Vector2(0.25f, -0.25f), transform.rotation);

        // TODO: This should be attacking enemyHunt.GetTarget();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bool IS_PLAYER_IN_BEFORE_ATTACK_DISTANCE = Vector2.Distance(player.transform.position, gameObject.transform.position) < attackRange;

        yield return new WaitForSeconds(attackSpeed / 2);
        bool IS_PLAYER_IN_ATTACK_DISTANCE = Vector2.Distance(player.transform.position, gameObject.transform.position) < attackRange;
        bool IS_FACING_PLAYER = (enemy.GetDirection() < 0 && player.transform.position.x - transform.position.x < 0) || (enemy.GetDirection() > 0 && player.transform.position.x - transform.position.x > 0);
        if (IS_PLAYER_IN_ATTACK_DISTANCE && IS_FACING_PLAYER) {
            player.GetComponent<Player>().TakeDamage(damage);
        }

        if (IS_PLAYER_IN_BEFORE_ATTACK_DISTANCE && !(IS_PLAYER_IN_ATTACK_DISTANCE && IS_FACING_PLAYER)) {
            Debug.Log("Dodged Btich!");
        }

        yield return new WaitForSeconds(attackSpeed / 2);
        enemy.SetIsAttacking(false);
    }
}
