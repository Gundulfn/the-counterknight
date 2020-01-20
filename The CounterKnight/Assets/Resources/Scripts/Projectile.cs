using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed;
    private bool isTargetSet = false;
    protected Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if(rb && isTargetSet)
        {
            rb.velocity = transform.up * projectileSpeed * Time.deltaTime;
        }                                                
    }

    protected void setObjectToTarget(Transform target, float speed)
    {
        projectileSpeed = speed;

        Vector3 targetVector = target.position - transform.position;
        transform.up = targetVector;
        isTargetSet = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
