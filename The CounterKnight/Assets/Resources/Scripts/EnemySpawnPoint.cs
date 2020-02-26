using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private GameObject enemyPrefab;
    private GameObject warningObj;

    private const decimal SPAWN_REDUCE_LIMIT = 2;
    private const decimal FIRST_SPAWN_RATE = 5;
    private static decimal defaultSpawnRate = FIRST_SPAWN_RATE;
    private decimal randomDelayLimit = 1;

    private decimal randomSpawnRate;

    private static bool isSpawning = true;
    private static int baseSpawnCount;

    private int spawnCount = 0;
    private int groupAttackCount = groupAttackAmount;
    private static int groupAttackAmount = 3;
    private const int GROUP_ATTACK_TIME = 5;

    void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy(DarkElf)");
        warningObj = transform.GetChild(0).gameObject;

        randomSpawnRate = getRandomSpawnDelay();
    }

    void Update()
    {
        if(isSpawning && Time.timeScale != 0)
        {
            //Reset Control
            if(defaultSpawnRate == FIRST_SPAWN_RATE && spawnCount != 0)
            {
                randomSpawnRate = getRandomSpawnDelay();
                spawnCount = 0;
                groupAttackCount = groupAttackAmount;
            }

            if(randomSpawnRate <= 0)
            {
                spawnEnemy();
                
                if(spawnCount % GROUP_ATTACK_TIME == 0 && spawnCount != 0)
                {
                    if(groupAttackCount == groupAttackAmount)
                    {
                        warningObj.GetComponent<Animation>().Play();
                    }

                    groupAttackCount--;

                    if(groupAttackCount == 0)
                    {
                        spawnCount++;
                        
                        groupAttackCount = groupAttackAmount;
                        randomSpawnRate = getRandomSpawnDelay();
                    }
                    else
                    {
                        randomSpawnRate = 1; // For faster attacks
                    }
                }
                else
                {
                    //Reducing defaulSpawnRate
                    if(defaultSpawnRate > SPAWN_REDUCE_LIMIT)
                    {
                        defaultSpawnRate -= 0.25M;
                    }

                    randomSpawnRate = getRandomSpawnDelay();  
                    spawnCount++;
                }    
            }
            
            randomSpawnRate -= (decimal)Time.deltaTime;
        }
    }

    private void spawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        baseSpawnCount++;
    }

    public static int getBaseSpawnCount()
    {
        return baseSpawnCount;
    }

    private decimal getRandomSpawnDelay()
    {
        return defaultSpawnRate + (decimal)Random.Range(0, decimal.ToSingle(randomDelayLimit))
                                + (transform.position.x < 0? 1.2m : 0); // Delay to spawns from left
    }
    
    public static void resetSpawnPoint()
    {
        isSpawning = true;
        baseSpawnCount = 0;
        defaultSpawnRate = FIRST_SPAWN_RATE;
    }

    public static void setSpawn()
    {
        isSpawning = !isSpawning;
    }

    public static void buffSpawnPoint()
    {
        groupAttackAmount += 1;
        defaultSpawnRate = 1.5m;
    }

    public static void increaseBaseSpawnCount()
    {
        baseSpawnCount++;
    }
}