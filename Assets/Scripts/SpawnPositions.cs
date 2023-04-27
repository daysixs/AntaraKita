using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    [SerializeField] private Transform[] pos;

    private int index;
    public int Index
    { get { return index; } }

    public Vector3 GetSpawnPos()
    {
        Vector3 position = pos[index++].position;

        if (index >= pos.Length)

        {
            index = 0;
        }

        return position;
    }
}