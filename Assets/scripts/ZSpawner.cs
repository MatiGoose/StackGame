using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSpawner : MonoBehaviour
{
    public GameObject BlockPrefab;
    public void SpawnBlock()
    {
        int state = (Random.Range(0, 2) == 0) ? 1 : -1;
        Vector3 SpawnBlockPosition = new Vector3(CurrentBlockMovement.CurrentBlock.transform.position.x, CurrentBlockMovement.CurrentBlock.transform.position.y + CurrentBlockMovement.CurrentBlock.transform.localScale.y, transform.position.z * state);
        GameObject NewBlock = Instantiate(BlockPrefab);


        NewBlock.transform.position = SpawnBlockPosition;
        NewBlock.transform.localScale = CurrentBlockMovement.CurrentBlock.transform.localScale;
        CurrentBlockMovement.MoveDirection = MoveDirection.z;
    }
}
