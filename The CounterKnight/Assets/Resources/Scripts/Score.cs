using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Text currentScoreText;
    private static Text bestScoreText;

    private static int score = 0;
    private static int bestScore;

    void Start()
    {
        currentScoreText = GameObject.Find("currentScoreText").GetComponent<Text>();
        bestScoreText = GameObject.Find("bestScoreText").GetComponent<Text>();

        bestScore = PlayerPrefs.GetInt("bestScore", 0);

        currentScoreText.text = "Score: " + score.ToString();
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    public static void increaseScore()
    {
        score += 1;
        currentScoreText.text = "Score: " + score.ToString();
    }

    public static void saveBestScore()
    {
        if(score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        score = 0;
    }

    public static void resetScore()
    {
        saveBestScore();
        currentScoreText.text = "Score: " + score.ToString();
    }
}
