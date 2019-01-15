using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private WayPoint _startWayPoint;
    [SerializeField] private WayPoint _endWayPoint;

    private Dictionary<Vector2Int, WayPoint> gridDictionary = new Dictionary<Vector2Int, WayPoint>();

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left,
    };

	void Start ()
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbours();
	}

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direaction in directions)
        {
            Vector2Int exploreCoordinates = _startWayPoint.GetGridPos() + direaction;
            if (gridDictionary.ContainsKey(exploreCoordinates) == true)
            {
                gridDictionary[exploreCoordinates].SetTopColor(Color.blue);
            }
        }
    }

    private void LoadBlocks()
    {
        var wayPoints = FindObjectsOfType<WayPoint>();

        foreach (WayPoint wayPoint in wayPoints)
        {
            var gridPos = wayPoint.GetGridPos();

            if (gridDictionary.ContainsKey(gridPos) == true)
            {
                Debug.LogWarning("Skip block" + wayPoint);
            }
            else
            {
                gridDictionary.Add(gridPos, wayPoint);
            }
        }
    }

    private void ColorStartAndEnd()
    {
        _startWayPoint.SetTopColor(Color.green);
        _endWayPoint.SetTopColor(Color.red);
    }
}
