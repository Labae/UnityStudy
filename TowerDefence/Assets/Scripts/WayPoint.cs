using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Color startColor { get; set; }
    public Color endColor { get; set; }
    [SerializeField] private Color exploredColor;

    public bool IsStartWayPoint { get; set; }
    public bool IsEndWayPoint { get; set; }

    private bool isExplored = false;
    private const int i_gridSize = 10;

    private Vector2Int gridPos;

    public bool IsExplored
    {
        get
        {
            return isExplored;
        }

        set
        {
            isExplored = value;
        }
    }

    public WayPoint ExploredFrom { get; set; }

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

    private void Update()
    {
        ProcessSetTopColor();
    }

    private void ProcessSetTopColor()
    {
        if(IsStartWayPoint == true)
        {
            SetTopColor(startColor);
        }
        else if(IsEndWayPoint == true)
        {
            SetTopColor(endColor);
        }
        else if (isExplored == true)
        {
            SetTopColor(exploredColor);
        }
    }

    public void SetTopColor(Color changeColor)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = changeColor;
    }
}
