using UnityEngine;

public class DestroyClass : MonoBehaviour
{
    private static int objArrayLength;

    public static void destroyObjectsByTag(string tag)
    {
        GameObject[] targetObjs = GameObject.FindGameObjectsWithTag(tag);
        objArrayLength = targetObjs.Length;

        foreach(GameObject targetObj in targetObjs)
        {
            Destroy(targetObj);
        }
    }

    public static int getObjectsCount()
    {
        return objArrayLength;
    }
}
