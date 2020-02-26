using UnityEngine;
using UnityEngine.UI;

public class LanguageOptions : MonoBehaviour
{
    public Text[] textArray;
    private TextAsset textFile;
    private string[] textElements;
    private static string lang;

    void Awake()
    {
        textFile = (TextAsset)Resources.Load("uiText");

        textElements = textFile.ToString().Split(';');

        lang = PlayerPrefs.GetString("Language", "en");
        changeLanguage();
    }

    public void changeLanguage()
    {        
        lang = PlayerPrefs.GetString("Language", "en");

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