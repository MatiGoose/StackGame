using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLogic : MonoBehaviour
{
    private Color TopColor;
    private Color BottomColor;

    private Color NewTopColor;
    private Color NewBottomColor;

    private Material GradientMaterial;

    private void Start()
    {
        RenderSettings.skybox.SetColor("_TopColor", Color.white);
        RenderSettings.skybox.SetColor("_BottomColor", Color.black);
    }
    void Awake()
    {
        TopColor = Random.ColorHSV();
        BottomColor = Random.ColorHSV();

        NewTopColor = TopColor;
        NewBottomColor = BottomColor;

        GameManager.SwitchColorsEvent += SwitchColors;
        GameManager.RestartEvent += Restart;
    }
    void Update()
    {
        SetColor();
    }

    private void SetColor()
    {
        TopColor = Color.Lerp(TopColor, NewTopColor, 0.5f * Time.deltaTime);
        BottomColor = Color.Lerp(BottomColor, NewBottomColor, 0.5f * Time.deltaTime);

        RenderSettings.skybox.SetColor("_TopColor", TopColor);
        RenderSettings.skybox.SetColor("_BottomColor", BottomColor);

    }
    private void Restart()
    {
        NewBottomColor = Random.ColorHSV();
        NewTopColor = Random.ColorHSV();
    }
    private void SwitchColors()
    {
        NewBottomColor = TopColor;
        NewTopColor = CurrentBlockMovement.CurrentColor;
    }
}
