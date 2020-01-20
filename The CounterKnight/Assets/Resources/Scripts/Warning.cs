using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    private AudioSource aud;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    public void playAud()
    {
        aud.Play();
    }
}
