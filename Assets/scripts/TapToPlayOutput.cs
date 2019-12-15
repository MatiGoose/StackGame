using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapToPlayOutput : MonoBehaviour
{
    private TextMeshProUGUI Text;

    private float CurrentAlpha = 1;
    void Start()
    {
        Text = GetComponent<TMPro.TextMeshProUGUI>();
        GameManager.SpawnBlockEvent += SpawnBlock;
        GameManager.RestartEvent += Restart;
    }
    void Update()
    {
        Text.color = Color.Lerp(Text.color, new Color(Text.color.r, Text.color.g, Text.color.b, CurrentAlpha), Time.deltaTime * 3);
    }
    private void SpawnBlock()
    {
        CurrentAlpha = 0;
    }
    private void Restart()
    {
        CurrentAlpha = 1;
    }
}
