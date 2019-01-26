using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool IsPlaceable = true;
    
    private const int i_gridSize = 10;

    private Vector2Int gridPos;

    public bool IsExplored { get; set; }

    public WayPoint ExploredFrom { get; set; }

    [SerializeField] private Tower towerPrefab;
    [SerializeField] private Transform towerParent;

    public int GetGridSize()
    {
        return i_gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
               Mathf.RoundToInt(transform.position.x / i_gridSize),
               Mathf.RoundToInt(transform.position.z / i_gridSize)
            );
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(IsPlaceable == true)
            {
                Instantiate(towerPrefab, transform.position, Quaternion.identity, towerParent);
            }
        }
    }
}
