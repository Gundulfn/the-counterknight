using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchBlock : MonoBehaviour
{
    public GameObject leftBlock;
    public GameObject rightBlock;
    
    void Update()
    {
        if(Input.touchCount > 0 && !IsPointerOverUIObject())
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

    private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        
        return results.Count > 0;
    }
}
