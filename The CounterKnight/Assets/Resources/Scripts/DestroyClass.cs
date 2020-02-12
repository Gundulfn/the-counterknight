using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClass : MonoBehaviour
{
    public static void destroyObjectsByTag(string tag)
    {
        GameObject[] targetObjs = GameObject.FindGameObjectsWithTag (tag);
        
        foreach(GameObject targetObj in targetObjs)
        {
            Destroy(targetObj);
        }
    }
}
