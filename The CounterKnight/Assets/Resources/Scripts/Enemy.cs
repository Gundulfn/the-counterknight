using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float enemySpeed = -0.05f;
    private float selfDestructTime;
    private BoxCollider2D boxCol;
    private GameObject arrowPrefab;

    void Start()
    {
        selfDestructTime = 4;
        boxCol = GetComponent<BoxCollider2D>();
        arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow(Demo)");

        if(selfDestructTime != 0)
        {
            StartCoroutine(destroyEnemy());
        }

        if(transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            boxCol.offset = new Vector2( -boxCol.offset.x, boxCol.offset.y);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(0, enemySpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().setArcherObj(gameObject);
        }    
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
