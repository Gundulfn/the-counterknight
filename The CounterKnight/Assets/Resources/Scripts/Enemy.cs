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
    private float shootDelay;

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

        shootDelay = Random.Range(2, 4);
        StartCoroutine(fireArrowAfterDelay());

        int supriseAttackChance = Random.Range(0, 3);

        if(supriseAttackChance == 0)
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
        if(col.gameObject.GetComponent<Lighting>())
        {
            animator.SetBool("isDead", true);
        }    
    }
    
    public void setEnemySpeed(float newSpeed)
    {
        enemySpeed = (newSpeed < 0) ? newSpeed : -newSpeed;
    }

    IEnumerator fireArrowAfterDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        animator.SetTrigger("attack");

        animator.SetBool("haveShot", true);
        // Wait for animation playing
        AnimationClip attackClip = (AnimationClip)Resources.Load("Animations/DarkElf/DarkElfAttack");
        yield return new WaitForSeconds( attackClip.length - 0.2f ); // -0.2f for shoot delay

        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().setArcherObj(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
