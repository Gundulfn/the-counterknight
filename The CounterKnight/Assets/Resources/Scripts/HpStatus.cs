using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpStatus : MonoBehaviour
{
    private static Image hpImage;
    private static string filePath = "UI/InGame/Heart/Heart(";

    void Start()
    {
        hpImage = GetComponent<Image>();
    }

    public static void setHpStatus(Character character)
    {
        hpImage.sprite = Resources.Load<Sprite>(filePath + character.getHp().ToString() + ")");
    }
}
