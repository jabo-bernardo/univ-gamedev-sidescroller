using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour
{
    public Enemy bossEnemy;
    public bool isDying;

    // Update is called once per frame
    void Update()
    {
        if (isDying) return;
        if (bossEnemy.health <= 50) {
            isDying = true;
            StartCoroutine(DeathScene());
        }
    }

    IEnumerator DeathScene() {
        yield return new WaitForSeconds(2f);
        StartCoroutine(FindObjectOfType<LevelLoader>().LoadLevel("Outro"));
    }
}
