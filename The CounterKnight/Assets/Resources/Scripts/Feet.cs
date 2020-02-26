using UnityEngine;

public class Feet : MonoBehaviour
{   
    public static Feet instance;
    private static AudioSource aud;
    
    void Start()
    {
        instance = this;
        aud = GetComponent<AudioSource>();
    }

    public void playAud()
    {
        if(UIHandler.soundOn)
        {
            aud.Play();
        }
    }
     
    public void stopAnimation()
    {
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().enabled = false;
    }

    public void startAnimation()
    {
        GetComponent<Animator>().enabled = true;
    }
}
