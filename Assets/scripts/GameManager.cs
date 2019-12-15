using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Stack<GameObject> Tower = new Stack<GameObject>();

    public GameObject XSpawner;
    public GameObject ZSpawner;

    public delegate void SwitchColors();
    public static event SwitchColors SpawnBlockEvent;

    public delegate void SpawnBlock();
    public static event SpawnBlock SwitchColorsEvent;

    public delegate void Restart();
    public static event Restart RestartEvent;

    public GameObject StartBlock;

    void Update()
    {
        if (/*Input.GetTouch(0).phase == TouchPhase.Began*/Input.GetButtonUp("Jump"))          // ?????
        {
            if (CurrentBlockMovement.GameOverState)
            {
                RestartEvent.Invoke();
                SpawnStartBlock();
                DeleteBlocks();
                CurrentBlockMovement.GameOverState = false;
                return;
            }
            if (CurrentBlockMovement.CurrentBlock != CurrentBlockMovement.PreviousBlock)
            {
                CurrentBlockMovement.CurrentBlock.Stop();
                SetNewColor();
                if (CurrentBlockMovement.ScoreCounter % 5 == 0 && CurrentBlockMovement.ScoreCounter != 0)
                    if (SwitchColorsEvent != null)
                        SwitchColorsEvent.Invoke();
            }
            
            if (!CurrentBlockMovement.GameOverState)
            {               
                if (SpawnBlockEvent != null)
                    SpawnBlockEvent.Invoke();
                Tower.Push(CurrentBlockMovement.CurrentBlock.gameObject);
                CurrentBlockMovement.PreviousBlock = CurrentBlockMovement.CurrentBlock;
                if (UnityEngine.Random.Range(0, 2) == 0)
                    XSpawner.GetComponent<XSpawner>().SpawnBlock();
                else
                    ZSpawner.GetComponent<ZSpawner>().SpawnBlock();
            }
        }       
    }
    private void DeleteBlocks()
    {
        while(Tower.Count != 0)
        {
            Destroy(Tower.Pop());
        }
    }
    private void SpawnStartBlock()
    {
        CurrentBlockMovement.CurrentBlock = null;
        CurrentBlockMovement.PreviousBlock = null;
        CurrentBlockMovement.ScoreCounter = 0;
        GameObject NewStartBlock = Instantiate(StartBlock);
    }
    private void SetNewColor()
    {
        CurrentBlockMovement.CurrentColor = new Color(CurrentBlockMovement.CurrentColor.r + UnityEngine.Random.Range(-0.06f, 0.06f), CurrentBlockMovement.CurrentColor.g + UnityEngine.Random.Range(-0.06f, 0.06f), CurrentBlockMovement.CurrentColor.b + UnityEngine.Random.Range(-0.06f, 0.06f));
    }
}