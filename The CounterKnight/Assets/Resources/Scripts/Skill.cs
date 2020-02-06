using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    private static float stack = 0;
    private static Image skillImg;
    private static Image skillSymbolImg;

    void Awake()
    {
        skillImg = GetComponent<Image>();
        skillSymbolImg = transform.GetChild(1).GetComponent<Image>();
    }

    public static void increaseStack()
    {
        stack++;
        skillImg.fillAmount = stack/10;
        
        if(stack == 10)
        {
            skillSymbolImg.color = new Color(255, 255, 255);
        }
        else
        {
            skillSymbolImg.color = new Color(127, 127, 127);
        }
    }

    public static void resetStack()
    {
        stack = 0;
        skillSymbolImg.color = new Color(127, 127, 127);
    }

    public static void useSkill()
    {
        
    }
}