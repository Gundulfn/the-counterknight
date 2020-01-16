using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private GameObject enemyPrefab;
    
    private const decimal SPAWN_REDUCE_LIMIT = 3;
    private const decimal FIRST_SPAWN_RATE = 5;
    private static decimal defaultSpawnRate = FIRST_SPAWN_RATE;
    private decimal extraSpawnDelay = 1;

    private decimal randomSpawnRate;

    void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy(DarkElf)");

        randomSpawnRate = defaultSpawnRate; // First attack delay is "defaultSpawnRate"
                                            // Then it will be random

        if(transform.position.x < 0)
        {
            randomSpawnRate += getRandomSpawnDelay(); // Delay to first spawn from left
        }
    }

    void FixedUpdate()
    {
        if(randomSpawnRate <= 0)
        {
            spawnEnemy();
            
            //Reducing defaulSpawnRate
            if(defaultSpawnRate > SPAWN_REDUCE_LIMIT)
            {
                defaultSpawnRate -= 0.2M;
            }

            randomSpawnRate = defaultSpawnRate + getRandomSpawnDelay() 
                              + (transform.position.x < 0? 1 : 0);   // Delay to spawns from left
        }

        randomSpawnRate -= (decimal)Time.deltaTime;
    }

    private void spawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private decimal getRandomSpawnDelay()
    {
        return (decimal)Random.Range(0, decimal.ToSingle(extraSpawnDelay));
    }

    public static void resetSpawnRate()
    {
        defaultSpawnRate = FIRST_SPAWN_RATE;
    }
}
