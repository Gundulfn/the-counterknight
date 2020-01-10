using System.Collections;
using UnityEngine;

public class Arrow : Projectile
{
    private float arrowSpeed = -300;
    private Transform target;
    private GameObject shooter;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        setObjectToTarget(target, arrowSpeed);
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
            
            GameObject lighting = Instantiate( (GameObject)Resources.Load(Lighting.lightingPrefabPath),
                                                transform.position, Quaternion.identity);
            
            lighting.GetComponent<Lighting>().setEnemyTarget(shooter.transform);

            Destroy(gameObject);
        }
    }

    public void setArcherObj(GameObject archer)
    {   
        shooter = archer;
    }
}
