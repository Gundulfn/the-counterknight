using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float arrowForwardSpeed = 300;
    private float arrowTurnSpeed = 3000;
    private Transform target;
    private Rigidbody2D rb;
    private GameObject shooter;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        Vector3 targetVector = target.position - transform.position;

        float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

        transform.Rotate(0, 0, 20*rotatingIndex);
    }

    void Update()
    {
        rb.velocity = -transform.up * arrowForwardSpeed * Time.deltaTime;                                                
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Character>().reduceHp();
            Destroy(gameObject);
        } 
        else if(col.gameObject.tag == "Block")
        {
            Destroy(shooter);
            Destroy(gameObject);
        }
    }

    public void setArcherObj(GameObject archer)
    {   
        shooter = archer;
    }
}
