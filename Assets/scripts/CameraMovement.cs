using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float BlockSize;
    public GameObject CurrentBlock;

    private Vector3 NewCameraPosition;
    private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
        NewCameraPosition = transform.position;

        BlockSize = CurrentBlock.transform.localScale.y;
        
        GameManager.SpawnBlockEvent += SpawnBlock;
        GameManager.RestartEvent += Restart;
        CurrentBlockMovement.GameOverEvent += GameOver;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, NewCameraPosition, 1f * Time.deltaTime);
    }
    private void SpawnBlock()
    {
        NewCameraPosition = new Vector3(transform.position.x, NewCameraPosition.y + BlockSize, transform.position.z);
    }
    private void GameOver()
    {
        NewCameraPosition = new Vector3(transform.position.x - CurrentBlockMovement.ScoreCounter/15, transform.position.y, transform.position.z - CurrentBlockMovement.ScoreCounter/15);
    }
    private void Restart()
    {
        NewCameraPosition = StartPosition;
        StartCoroutine("GoToSpawn");
    }
    private IEnumerator GoToSpawn()
    {
        while(NewCameraPosition.y <= transform.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, NewCameraPosition, 1f * Time.deltaTime);
            yield return null;
        }
        yield return 0;
    }
}
