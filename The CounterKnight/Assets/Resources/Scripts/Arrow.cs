using UnityEngine;

public class Arrow : Projectile
{
    private float arrowSpeed = 300;
    private Transform target;
    private GameObject shooter;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        setObjectToTarget(target, arrowSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col);

        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Character>().reduceCharHp();
            
            Destroy(gameObject);
        } 
        else if(col.gameObject.CompareTag("Block"))
        {
            Score.increaseScore();
            Skill.increaseStack();
            
            if(col.gameObject.transform.position.x < 0)
            {
                Character.triggerLeftBlock();
            }
            else
            {
                Character.triggerRighttBlock();
            }
            
            // Counter Attack
            Lighting.createLighting(transform.position, shooter.transform);
            
            Destroy(gameObject);
        }
    }

    public void setArcherObj(GameObject archer)
    {   
        shooter = archer;
    }
}
