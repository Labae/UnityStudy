using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private WayPoint _startWaypoint;
    [SerializeField] private WayPoint _endWaypoint;

    private Dictionary<Vector2Int, WayPoint> gridDictionary = new Dictionary<Vector2Int, WayPoint>();
    private Queue<WayPoint> waypointQueue = new Queue<WayPoint>();

    private bool isSearching = true;
    private WayPoint searchCenter;      // the current searchCenter

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left,
    };

    void Start()
    {
        LoadBlocks();
        SetStartAndEndWayPoint();
        PathFind();
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

    private void SetStartAndEndWayPoint()
    {
        _startWaypoint.IsStartWayPoint = true;
        _endWaypoint.IsEndWayPoint = true;
        _startWaypoint.startColor = Color.red;
        _endWaypoint.endColor = Color.green;
    }

    private void PathFind()
    {
        waypointQueue.Enqueue(_startWaypoint);

        while (waypointQueue.Count > 0 && isSearching == true)
        {
            searchCenter = waypointQueue.Dequeue();
            searchCenter.IsExplored = true;

            HalfIfEndFound();
            ExploreNeighbours();
        }

        Debug.Log("Finished pathFinding");
    }

    private void HalfIfEndFound()
    {
        if(searchCenter == _endWaypoint)
        {
            isSearching = false;
            Debug.Log("Searching from end node, therefore stop");
        }
    }

    private void ExploreNeighbours()
    {
        if(isSearching == false)
        {
            return;
        }

        foreach (Vector2Int direaction in directions)
        {
            Vector2Int exploreCoordinates = searchCenter.GetGridPos() + direaction;

            if (gridDictionary.ContainsKey(exploreCoordinates) == true)
            {
                QueueNewNeighbour(exploreCoordinates);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int exploreCoordinates)
    {
        WayPoint neighbour = gridDictionary[exploreCoordinates];
        if (neighbour.IsExplored == true || waypointQueue.Contains(neighbour) == true)
        {

        }
        else
        {
            waypointQueue.Enqueue(neighbour);
            neighbour.ExploredFrom = searchCenter;
        }
    }
}
