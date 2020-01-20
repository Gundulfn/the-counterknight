using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : Projectile
{
    public static string lightingPrefabPath = "Prefabs/Lighting";
    private static float lightingSpeed = 800;

    void Start()
    {
        GetComponent<AudioSource>().mute = !UIHandler.soundOn;
    }

    public static void createLighting(Vector3 pos, Transform enemyTarget)
    {
        GameObject lightingObj = Instantiate( (GameObject)Resources.Load(lightingPrefabPath),
                                                pos, Quaternion.identity);
            
        Lighting lighting = lightingObj.GetComponent<Lighting>();           
        
        lighting.setObjectToTarget(enemyTarget, lightingSpeed);

        lighting.GetComponent<ParticleSystem>().Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Enemy>())
        {
            // Fake Destroy(gameObject)
            Destroy( GetComponent<BoxCollider2D>() );
            Destroy( rb );
            Destroy( GetComponent<ParticleSystem>() );

            GetComponent<SpriteRenderer>().sprite = null;
            
            StartCoroutine(destroyObjAfterAudio());
        }
    }

    IEnumerator destroyObjAfterAudio()
    {
        AudioClip audClip = GetComponent<AudioSource>().clip;
        yield return new WaitForSeconds(audClip.length);
        Destroy(gameObject);
    }
}
