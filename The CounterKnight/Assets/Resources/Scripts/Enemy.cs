using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float enemySpeed = -0.05f;
    private float selfDestructTime;
    
    void Start()
    {
        selfDestructTime = 4;

        if(selfDestructTime != 0)
        {
            StartCoroutine(destroyEnemy());
        }

        if(transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Update()
    {
        transform.Translate(0, enemySpeed, 0);
    }

    private IEnumerator destroyEnemy()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }

    public void setEnemySpeed(float newSpeed)
    {
        enemySpeed = (newSpeed < 0) ? newSpeed : -newSpeed;
    }
}
