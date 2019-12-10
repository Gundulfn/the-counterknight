using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpStatus : MonoBehaviour
{
    private Character character;
    private Text hpText;

    void Awake()
    {
        character = GameObject.Find("Character(Demo)").GetComponent<Character>();
        hpText = GetComponent<Text>();
    }

    void Update()
    {
        hpText.text = character.getHp().ToString();
    }
}
