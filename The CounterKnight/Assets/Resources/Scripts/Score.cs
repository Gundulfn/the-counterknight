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
        currentScoreText = GameObject.Find("currentScoreText").GetComponent<Text>();
        bestScoreText = GameObject.Find("bestScoreText").GetComponent<Text>();

        currentScoreText.text = currentScoreString + score.ToString();
        bestScoreText.text = bestScoreString + bestScore.ToString();
    }

    public static void increaseScore()
    {
        score += 1;
        currentScoreText.text = currentScoreString + score.ToString();
    }

    public static void saveBestScore()
    {
        if(score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
            bestScoreText.text = bestScoreString + score.ToString();
        }
    }

    public static void resetScore()
    {
        saveBestScore();
        score = 0;
        currentScoreText.text = currentScoreString + score.ToString();
    }

    public static void changeLanguage(string lang)
    {
        currentScoreString = (lang == "en"? "Score: ": "Skor: ");
        bestScoreString = (lang == "en"? "Best Score: ": "Rekor: ");

        if(GameObject.Find("Canvas").GetComponent<Score>().enabled)
        {
            currentScoreText.text = currentScoreString + score.ToString();
            bestScoreText.text = bestScoreString + bestScore.ToString();
        }
    }
}