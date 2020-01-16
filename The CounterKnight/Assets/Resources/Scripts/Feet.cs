using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private static AudioSource aud;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void playAud()
    {
        if(UIHandler.soundOn)
        {
            aud.Play();
        }
    }
}
