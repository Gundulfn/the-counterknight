using UnityEngine;
using UnityEngine.UI;

public class LanguageOptions : MonoBehaviour
{
    public Text[] textArray;
    private TextAsset textFile;
    private string[] textElements;

    void Awake()
    {
        textFile = (TextAsset)Resources.Load("uiText");

        textElements = textFile.ToString().Split(';');

        string lang = PlayerPrefs.GetString("Language", "en");
        changeLanguage(lang);
    }

    public void changeLanguage(string lang)
    {        
        if(lang == "en")
        {
            for(int i = 0; i < textArray.Length; i++)
            {
                textArray[i].text = textElements[ 2 * i ];
            }
        }
        else
        {
            for(int i = 0; i < textArray.Length; i++)
            {
                textArray[i].text = textElements[ 2 * i + 1 ];
            }
        }

        Score.changeLanguage(lang);
    }
}