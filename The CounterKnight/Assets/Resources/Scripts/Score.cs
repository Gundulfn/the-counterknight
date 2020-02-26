using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Text currentScoreText;
    private static Text bestScoreText;

    private static int score = 0;
    private static int bestScore;

    private static string currentScoreString = "Score: ";
    private static string bestScoreString = "Best Score: ";
    
    void Awake()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
    }   

    void Start()
    {
        setScoreTexts();
    }
    
    public static void increaseScore(int amount = 1)
    {
        score += amount;
        currentScoreText.text = currentScoreString + score.ToString();
    }

    public static int getScore()
    {
        return score;
    }

    public static void saveBestScore()
    {
        if(score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            bestScore = score;
            setScoreTexts();
        }
    }

    public static void resetScore()
    {
        saveBestScore();
        score = 0;
        setScoreTexts();
    }

    public static void changeLanguage(string lang)
    {
        currentScoreString = (lang == "en"? "Score: ": "Skor: ");
        bestScoreString = (lang == "en"? "Best Score: ": "Rekor: ");
    }

    public static void setScoreTexts()
    {
        if(!currentScoreText ||!bestScoreText)
        {
            currentScoreText = GameObject.Find("currentScoreText").GetComponent<Text>();
            bestScoreText = GameObject.Find("bestScoreText").GetComponent<Text>();
        }

        currentScoreText.text = currentScoreString + score.ToString();
        bestScoreText.text = bestScoreString + bestScore.ToString();
    }
}