using UnityEngine;

public class Arrow : Projectile
{
    private int arrowDamage;
    private const int ENEMY_ARROW_DAMAGE = 1;
    private const int BOSS_ARROW_DAMAGE = 2;
    
    private float arrowSpeed;
    private const float ENEMY_ARROW_SPEED = 300;
    private const float BOSS_ARROW_SPEED = 600;

    private Transform target;
    private GameObject shooter = null;
    private Transform bossTransform;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(bossTransform)
        {
            arrowSpeed = BOSS_ARROW_SPEED;
            arrowDamage = BOSS_ARROW_DAMAGE;
        }
        else
        {
            arrowSpeed = ENEMY_ARROW_SPEED;
            arrowDamage = ENEMY_ARROW_DAMAGE;
        }

        setObjectToTarget(target, arrowSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), col);

        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Character>().reduceCharHp(arrowDamage);
            
            Destroy(gameObject);
        } 
        else if(col.gameObject.CompareTag("Block"))
        {
            Skill.instance.increaseStack();
            
            if(col.gameObject.transform.position.x < 0)
            {
                Character.triggerLeftBlock();
            }
            else
            {
                Character.triggerRightBlock();
            }
            
            // Counter Attack
            if(shooter)
            {
                Lighting.createLighting(transform.position, shooter.transform);                
            }
            else if(bossTransform)
            {
                Lighting.createLighting(transform.position, bossTransform);
            }
            
            Destroy(gameObject);
        }
    }

    public void setArcherObj(GameObject archer)
    {
        shooter = archer;
    }

    public void setBossTransform(Transform bossObj)
    {
        bossTransform = bossObj;
    }
}