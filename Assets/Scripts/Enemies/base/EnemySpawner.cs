using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public int numberOfEnemiesPerSpawn = 1;
    public bool isLooping;
    public Vector2 spawnIntervalRange = new Vector2(0, 5);
    public bool noInitialInterval = true;
    private float currentInterval;

    void Start()
    {
        currentInterval = noInitialInterval ? 0 : Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        yield return new WaitForSeconds(currentInterval);
        for (int i = 0; i < numberOfEnemiesPerSpawn; i++) {
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        if (isLooping) {
            currentInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            StartCoroutine(SpawnEnemies());
        }
    }
}
