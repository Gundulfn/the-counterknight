using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float enemySpeed = -2;

    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    private GameObject arrowPrefab;

    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow(Demo)");
        
        rb.gravityScale = 0;

        if(transform.position.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            boxCol.offset = new Vector2( -boxCol.offset.x, boxCol.offset.y);
        }

        // Hidden random attack 
        int x = Random.Range(0, 3);

        if(x == 0)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().setArcherObj(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * enemySpeed * Time.deltaTime;

        transform.Translate(0, enemySpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Arrow>().setArcherObj(gameObject);
        }    
    }

    public void setEnemySpeed(float newSpeed)
    {
        enemySpeed = (newSpeed < 0) ? newSpeed : -newSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
