using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    public GameObject backgroundObj;

    void Awake()
    {
        Camera.main.orthographicSize = backgroundObj.transform.localScale.x * Screen.height 
                                        / Screen.width * 0.5f;
    }
}