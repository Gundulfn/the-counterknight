using UnityEngine;

public class TouchBlock : MonoBehaviour
{
    public GameObject leftBlock;
    public GameObject rightBlock;
    
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
        
            if(touchPos.x < 0)
            {
                setBlockActivity(true, false);
            }
            else
            {
                setBlockActivity(false, true);
            }
        }
    }
    
    private void setBlockActivity(bool state1, bool state2)
    {
        leftBlock.SetActive(state1);
        rightBlock.SetActive(state2);
        Character.setLeftHold(state1);
    }
}
