using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float arrowForwardSpeed = 300;
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
        float rotate = Map(targetVector.y, -9, -3, 5, 18);

        transform.Rotate(0, 0, rotate * rotatingIndex);

        StartCoroutine(destroyArrow());
    }

    void Update()
    {
        rb.velocity = -transform.up * arrowForwardSpeed * Time.deltaTime;                                                
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Character>().reduceCharHp();
            
            Destroy(gameObject);
        } 
        else if(col.gameObject.tag == "Block")
        {
            Score.increaseScore();

            if(col.gameObject.transform.position.x < 0)
            {
                Character.triggerLeftBlock();
            }
            else
            {
                Character.triggerRighttBlock();
            }
            
            Destroy(shooter);
            Destroy(gameObject);
        }
    }

    private IEnumerator destroyArrow()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public void setArcherObj(GameObject archer)
    {   
        shooter = archer;
    }
}
