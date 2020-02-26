using UnityEngine;
using System.Collections;

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
        if(col.gameObject.GetComponent<Enemy>() || col.gameObject.GetComponent<Boss>())
        {
            if(col.gameObject.GetComponent<Enemy>())
            {
                Score.increaseScore();
            }

            Destroy( GetComponent<BoxCollider2D>() );
            Destroy( GetComponent<ParticleSystem>() );
            Destroy( GetComponent<Rigidbody2D>() );
            
            GetComponent<SpriteRenderer>().sprite = null;
            StartCoroutine(destroyAfterAudioEnds());
        }
    }

    private IEnumerator destroyAfterAudioEnds()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }
}