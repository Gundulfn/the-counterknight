using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private GameObject enemyPrefab;
    private float defaultSpawnRate = 100;
    private float randomSpawnRate;
    private float reduceLimit = 10;

    void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy(Demo)");

        randomSpawnRate = defaultSpawnRate + Random.Range(0, reduceLimit) * -1;
    }

    void FixedUpdate()
    {
        if(randomSpawnRate <= 0)
        {
            spawnEnemy();
            randomSpawnRate = defaultSpawnRate + Random.Range(0, reduceLimit) * -1;
        }

        randomSpawnRate--;
    }

    private void spawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
