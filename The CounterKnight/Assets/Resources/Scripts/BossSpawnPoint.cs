using UnityEngine;

public class BossSpawnPoint : MonoBehaviour
{
    private static GameObject bossPrefab1;
    private static int BOSS_WAVE_LIMIT = 50;
    private static int GHOST_LIMIT = BOSS_WAVE_LIMIT * 3;
    private static bool isBossEntered = false;

    void Update()
    {
        if( Time.timeScale != 0 && 
            !isBossEntered && !GameObject.FindObjectOfType<Boss>())
        {
            int spawnCount = EnemySpawnPoint.getBaseSpawnCount();
            checkBossWave(spawnCount);
        }
    }

    public static void spawnBoss()
    {
        bossPrefab1 = (GameObject)Resources.Load("Prefabs/Boss(DarkElf)");
        Instantiate(bossPrefab1, bossPrefab1.transform.position, Quaternion.identity);   
        
        isBossEntered = true;
    }

    public static void continueWave()
    {
        Score.increaseScore();
        Feet.instance.startAnimation();
        Background.setBackgroundActive();
        UIHandler.changeMusicClip();
        
        EnemySpawnPoint.setSpawn();
        EnemySpawnPoint.increaseBaseSpawnCount();

        if(EnemySpawnPoint.getBaseSpawnCount() >= GHOST_LIMIT)
        {
            Enemy.setGhostColor();
        }

        isBossEntered = false;
    }

    public static void checkBossWave(int spawnCount)
    {
        if(spawnCount != 0 && spawnCount % BOSS_WAVE_LIMIT == 0)
        {
            EnemySpawnPoint.setSpawn();
            spawnBoss();
        }
    }

    public static void resetSpawnPoint()
    {
        isBossEntered = false;
    }
}