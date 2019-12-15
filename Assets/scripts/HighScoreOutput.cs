using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreOutput : MonoBehaviour
{

    public static int HighScore = 0;
    private TextMeshProUGUI TextScore;

    private float CurrentAlpha = 0;

    void Start()
    {
        TextScore = GetComponent<TMPro.TextMeshProUGUI>();
        CurrentBlockMovement.GameOverEvent += GameOver;
        GameManager.RestartEvent += Restart;
    }
    private void Update()
    {
        TextScore.color = Color.Lerp(TextScore.color, new Color(TextScore.color.r, TextScore.color.g, TextScore.color.b, CurrentAlpha), Time.deltaTime);
    }
    private void GameOver()
    {
        TextScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        CurrentAlpha = 1;
    }
    private void Restart()
    {
        CurrentAlpha = 0;
    }
}
