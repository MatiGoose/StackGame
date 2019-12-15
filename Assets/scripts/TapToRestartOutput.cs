using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapToRestartOutput : MonoBehaviour
{
    private TextMeshProUGUI Text;
    private float CurrentAlpha = 0;
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
        CurrentBlockMovement.GameOverEvent += GameOver;
        GameManager.RestartEvent += Restart;
    }

    void Update()
    {
        Text.color = Color.Lerp(Text.color, new Color(Text.color.r, Text.color.g, Text.color.b, CurrentAlpha), Time.deltaTime * 2);
    }
    void GameOver()
    {
        CurrentAlpha = 1;
    }
    void Restart()
    {
        CurrentAlpha = 0;
    }
}
