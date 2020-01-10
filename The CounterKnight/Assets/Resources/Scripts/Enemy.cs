using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool destroyEnemy = false;
    private float enemySpeed = -2;
    
    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    private GameObject arrowPrefab;

    private Animator animator;

    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        arrowPrefab = (GameObject)Resources.Load("Prefabs/Arrow");
        
        rb.gravityScale = 0;

        if(transform.position.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            boxCol.offset = new Vector2( -boxCol.offset.x, boxCol.offset.y);
        }

        animator = GetComponent<Animator>();

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
        if(destroyEnemy)
        {
            Destroy(gameObject);
        }
        
        rb.velocity = transform.up * enemySpeed * Time.deltaTime;

        transform.Translate(0, enemySpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            animator.SetTrigger("attack");
            StartCoroutine(fireArrowUntilAnimation());

            animator.SetBool("haveShot", true);
        }    
    }
    
    public void setEnemySpeed(float newSpeed)
    {
        enemySpeed = (newSpeed < 0) ? newSpeed : -newSpeed;
    }

    public void killEnemy()
    {
        animator.SetBool("isDead", true);
    }

    IEnumerator fireArrowUntilAnimation()
    {
        AnimationClip attackClip = (AnimationClip)Resources.Load("Animations/DarkElf/DarkElfAttack");
        yield return new WaitForSeconds( attackClip.length - 0.2f ); // -0.2f for arrow shoot delay

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().setArcherObj(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
