using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClass : MonoBehaviour
{
    public static void destroyObjectsByTag(string tag)
    {
        GameObject[] targetObjs = GameObject.FindGameObjectsWithTag (tag);
        
        for(var i = 0 ; i < targetObjs.Length ; i ++)
        {
            Destroy(targetObjs[i]);
        }
    }
}
