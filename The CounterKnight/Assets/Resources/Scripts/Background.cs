using UnityEngine;

public class Background : MonoBehaviour
{
    private const float INCREASE_AMOUNT = 0.03f;
    private const float INCREASE_LIMIT = 0.3f;
    private const float DEFAULT_SPEED = 0.12f;
    private static float scrollSpeed = DEFAULT_SPEED;

    private static Background instance;
    private static bool backgroundActive = true;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(Time.timeScale != 0)
        {
            Vector2 offset = new Vector2(0, Time.time * scrollSpeed);

            GetComponent<Renderer>().material.mainTextureOffset = offset;
        }
    }

    public static void increaseSpeed()
    {
        if(scrollSpeed < INCREASE_LIMIT)
        {
            scrollSpeed += INCREASE_AMOUNT;
        }
    }

    public static void resetBackground()
    {
        scrollSpeed = DEFAULT_SPEED;
        backgroundActive = true;
        instance.enabled = true;
    }
    
    public static void setBackgroundActive()
    {
        backgroundActive = !backgroundActive;
        instance.enabled = backgroundActive;
    }
}