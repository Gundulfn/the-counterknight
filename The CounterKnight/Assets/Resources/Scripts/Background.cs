using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float scrollSpeed = 0.15f;
    
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

    public void setScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
