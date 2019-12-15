using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class XSpawner : MonoBehaviour
{
    public GameObject BlockPrefab;
    public void SpawnBlock()
    {
        int state = (Random.Range(0, 2) == 0) ? 1 : -1;
        Vector3 SpawnBlockPosition = new Vector3(transform.position.x * state, CurrentBlockMovement.CurrentBlock.transform.position.y + CurrentBlockMovement.CurrentBlock.transform.localScale.y, CurrentBlockMovement.CurrentBlock.transform.position.z);
        GameObject NewBlock = Instantiate(BlockPrefab);

        NewBlock.transform.position = SpawnBlockPosition;
        NewBlock.transform.localScale = CurrentBlockMovement.CurrentBlock.transform.localScale;
        CurrentBlockMovement.MoveDirection = MoveDirection.x;
    }
}

