using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CurrentBlockMovement : MonoBehaviour
{
    public static CurrentBlockMovement CurrentBlock { set; get; }
    public static CurrentBlockMovement PreviousBlock { set; get; }

    public static MoveDirection MoveDirection { set; get; }
    public static int ScoreCounter = 0;

    public GameObject StartBlock;

    private Renderer CurrentBlockRenderer;
    public static Color CurrentColor;

    public Vector3 StartPoint;
    public Vector3 SecondPoint;

    public delegate void GameOver();
    public static event GameOver GameOverEvent;
    public static bool GameOverState;

    int direction = 1;

    [SerializeField]
    public float speed = 4f;

    void Start()
    {
        if (PreviousBlock == null)
        {
            PreviousBlock = StartBlock.GetComponent<CurrentBlockMovement>();
            CurrentColor = UnityEngine.Random.ColorHSV();
        }
        GetComponent<Renderer>().material.SetColor("_Color", CurrentColor);
        CurrentBlock = this;
        StartPoint = CurrentBlock.gameObject.transform.position;
        SecondPoint = StartPoint;
        if (MoveDirection == MoveDirection.x)
            SecondPoint.x *= -1;
        else
            SecondPoint.z *= -1;
    }

    void Update()
    {
        if (MoveDirection == MoveDirection.x)
        {
            if (StartPoint.x < 0)
            {
                if (transform.position.x <= StartPoint.x)
                {
                    direction = 1;
                }
                if (transform.position.x >= SecondPoint.x)
                {
                    direction = -1;
                }
            }
            else
            {
                if (transform.position.x >= StartPoint.x)
                {
                    direction = -1;
                }
                if (transform.position.x <= SecondPoint.x)
                {
                    direction = 1;
                }
            }
            transform.position += Vector3.right * speed * direction * Time.deltaTime;           
        }
        else
        {

            if(StartPoint.z < 0)
            {
                if (transform.position.z <= StartPoint.z)
                {
                    direction = 1;
                }
                if (transform.position.z >= SecondPoint.z)
                {
                    direction = -1;
                }
            }
            else
            {
                if (transform.position.z >= StartPoint.z)
                {
                    direction = -1;
                }
                if (transform.position.z <= SecondPoint.z)
                {
                    direction = 1;
                }
            }
            transform.position += Vector3.forward * speed * direction * Time.deltaTime;
        }
        
    }

    public void Stop()
    {
        float SplitSize = 0;
        speed = 0;
        if(MoveDirection == MoveDirection.x)
        {

            SplitSize = transform.position.x - PreviousBlock.transform.position.x;
            if (Mathf.Abs(SplitSize) >= PreviousBlock.transform.localScale.x)
            {

                if (GameOverState)
                    return;
                GameOverState = true;
                var LastBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
                LastBlock.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                LastBlock.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                gameObject.SetActive(false);
                LastBlock.AddComponent<Rigidbody>();
                LastBlock.GetComponent<Renderer>().material.SetColor("_Color", CurrentColor);
                Destroy(LastBlock, 1f);
                GameOverEvent.Invoke();
                return;
            }
        }
        else
        {
            SplitSize = transform.position.z - PreviousBlock.transform.position.z;
            if (Mathf.Abs(SplitSize) >= PreviousBlock.transform.localScale.z)
            {
                if (GameOverState)
                    return;
                GameOverState = true;
                var LastBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
                LastBlock.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                LastBlock.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                gameObject.SetActive(false);
                LastBlock.AddComponent<Rigidbody>();
                LastBlock.GetComponent<Renderer>().material.SetColor("_Color", CurrentColor);
                Destroy(LastBlock, 1f);
                GameOverEvent.Invoke();
                return;
            }
        }

        ScoreCounter++;
        SplitBlock(SplitSize);
        
    }

    private void SplitBlock(float SplitSize)
    {
        int direction = 1;
        if (SplitSize < 0)
        {
            direction = -1;
        }
        Vector3 NewScaleCurrentBlock = transform.localScale;
        Vector3 NewPositionCurrentBlock = transform.position;

        Vector3 NewScaleCuttedBlock = transform.localScale;
        Vector3 NewPositionCuttedBlock = transform.position;

        if (MoveDirection == MoveDirection.x)
        {
            if (Mathf.Abs(CurrentBlock.gameObject.transform.position.x - PreviousBlock.gameObject.transform.position.x) <= 0.05f)
            {
                Vector3 NewPosition = new Vector3(PreviousBlock.gameObject.transform.position.x, transform.position.y, PreviousBlock.gameObject.transform.position.z);
                CurrentBlock.gameObject.transform.position = NewPosition;
                return;
            }
            NewScaleCurrentBlock.x -= Mathf.Abs(SplitSize);
            NewPositionCurrentBlock.x -= SplitSize / 2;

            NewScaleCuttedBlock.x -= NewScaleCurrentBlock.x;
            NewPositionCuttedBlock.x += NewScaleCurrentBlock.x/2 * direction;
        }
        else
        {
            if (Mathf.Abs(CurrentBlock.gameObject.transform.position.z - PreviousBlock.gameObject.transform.position.z) <= 0.05f)
            {
                Vector3 NewPosition = new Vector3(PreviousBlock.gameObject.transform.position.x, transform.position.y, PreviousBlock.gameObject.transform.position.z);
                CurrentBlock.gameObject.transform.position = NewPosition;
                return;
            }
            NewScaleCurrentBlock.z -= Mathf.Abs(SplitSize);
            NewPositionCurrentBlock.z -= SplitSize / 2;

            NewScaleCuttedBlock.z -= NewScaleCurrentBlock.z;
            NewPositionCuttedBlock.z += NewScaleCurrentBlock.z/2 * direction;
        }
        

        transform.localScale = new Vector3(NewScaleCurrentBlock.x, NewScaleCurrentBlock.y, NewScaleCurrentBlock.z);
        transform.position = new Vector3(NewPositionCurrentBlock.x, NewPositionCurrentBlock.y, NewPositionCurrentBlock.z);

        SpawnCuttedBlock(NewScaleCuttedBlock, NewPositionCuttedBlock);
    }

    private void SpawnCuttedBlock(Vector3 NewScaleCuttedBlock, Vector3 NewPositionCuttedBlock)
    {
        var CuttedBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);

        CuttedBlock.transform.localScale = new Vector3(NewScaleCuttedBlock.x, NewScaleCuttedBlock.y, NewScaleCuttedBlock.z);
        CuttedBlock.transform.position = new Vector3(NewPositionCuttedBlock.x, NewPositionCuttedBlock.y, NewPositionCuttedBlock.z);
        CuttedBlock.AddComponent<Rigidbody>();
        CuttedBlock.GetComponent<Renderer>().material.SetColor("_Color", CurrentColor);
        Destroy(CuttedBlock, 2f);
    }
}
