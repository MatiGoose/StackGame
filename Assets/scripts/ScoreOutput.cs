using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreOutput : MonoBehaviour
{
    private TextMeshProUGUI TextScore;
    private float CurrentAlpha = 0;

    void Start()
    {
        TextScore = GetComponent<TMPro.TextMeshProUGUI>();
        CurrentBlockMovement.GameOverEvent += GameOver;
        GameManager.SpawnBlockEvent += SpawnBlock;
        GameManager.RestartEvent += Restart;
    }
    void Update()
    {
        TextScore.color = Color.Lerp(TextScore.color, new Color(TextScore.color.r, TextScore.color.g, TextScore.color.b, CurrentAlpha), Time.deltaTime);
    }
    private void SpawnBlock()
    {
        CurrentAlpha = 1;        
        TextScore.text = CurrentBlockMovement.ScoreCounter.ToString();
    }
    private void GameOver()
    {
        if(CurrentBlockMovement.ScoreCounter > HighScoreOutput.HighScore)
        {
            PlayerPrefs.SetInt("HighScore", CurrentBlockMovement.ScoreCounter);
            PlayerPrefs.Save();
            HighScoreOutput.HighScore = CurrentBlockMovement.ScoreCounter;
        }
    }
    private void Restart()
    {
        CurrentAlpha = 0;
    }
}
