using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectileSpeed;
    private bool isTargetSet = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        if(isTargetSet)
        {
            rb.velocity = transform.up * projectileSpeed * Time.deltaTime;
        }                                                
    }

    protected void setObjectToTarget(Transform target, float speed)
    {
        projectileSpeed = speed;

        Vector3 targetVector = target.position - transform.position;
        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
        float rotate = (projectileSpeed < 0)? 
                        Map(targetVector.y, -9, -3, 5, 15) : Map(targetVector.y, 0, 6.3f, -50, -10);

        transform.Rotate(0, 0, rotate * rotatingIndex);
        isTargetSet = true;
    }

    private float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
