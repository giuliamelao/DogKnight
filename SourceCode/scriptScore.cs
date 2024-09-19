using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public int scoreToWin = 15;

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        Debug.Log("Adicionando gema: " + value);
        score += value;
        UpdateScoreText();

        if (score >= scoreToWin)
        {
            SceneManager.LoadScene("WinnerScene");
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Gemas: " + score;
    }

    
}
