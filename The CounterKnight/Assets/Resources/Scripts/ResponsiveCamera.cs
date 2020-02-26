using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    public GameObject backgroundObj;
    private const float SPECIAL_HEIGHT = 12f;
    
    void Awake()
    {
        if(Screen.width / Screen.height < 0.5f)
        {
            backgroundObj.transform.localScale= new Vector3(backgroundObj.transform.localScale.x, 
                                                            SPECIAL_HEIGHT, 0);
        }

        Camera.main.orthographicSize = backgroundObj.transform.localScale.x * Screen.height 
                                        / Screen.width * 0.5f;
    }
}