using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private const int i_gridSize = 10;

    private Vector2Int gridPos;
    
    public int GetGridSize()
    {
        return i_gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
               Mathf.RoundToInt(transform.position.x / i_gridSize) * i_gridSize,
               Mathf.RoundToInt(transform.position.z / i_gridSize) * i_gridSize
            );
    }
}
