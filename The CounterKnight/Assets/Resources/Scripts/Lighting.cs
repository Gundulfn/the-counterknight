using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : Projectile
{
    public static string lightingPrefabPath = "Prefabs/Lighting";
    private float lightingSpeed = 800;
    
    public void setEnemyTarget(Transform enemyTarget)
    {
        setObjectToTarget(enemyTarget, lightingSpeed);

        GetComponent<ParticleSystem>().Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Enemy>())
        {
            Destroy(gameObject);
        }
    }
}
