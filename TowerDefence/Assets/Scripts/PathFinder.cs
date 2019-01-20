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
    private List<WayPoint> waypointPathList = new List<WayPoint>();

    private bool isSearching = true;
    private WayPoint searchCenter;      // the current searchCenter

    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.right,
        Vector2Int.left,
    };
    
    public List<WayPoint> GetWaypointPathList()
    {
        if (waypointPathList.Count == 0)
        {
            CalculatePath();
        }

        return waypointPathList;
        
    }

    private void CalculatePath()
    {
        LoadBlocks();
        SetStartAndEndWayPoint();
        BFS();
        CreatePath();
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
        if(_startWaypoint == null)
        {
            Debug.Log("StartWaypoint is null.");
        }
        else if(_endWaypoint == null)
        {
            Debug.Log("EndWaypoint is null");
        }


        _startWaypoint.IsStartWayPoint = true;
        _endWaypoint.IsEndWayPoint = true;
        _startWaypoint.startColor = Color.red;
        _endWaypoint.endColor = Color.green;
    }

    private void BFS()
    {
        waypointQueue.Enqueue(_startWaypoint);

        while (waypointQueue.Count > 0 && isSearching == true)
        {
            searchCenter = waypointQueue.Dequeue();

            HalfIfEndFound();
            ExploreNeighbours();
            searchCenter.IsExplored = true;
        }

        Debug.Log("Finished pathFinding");  // todo : remove log
    }

    private void HalfIfEndFound()
    {
        if(searchCenter == _endWaypoint)
        {
            isSearching = false;
            Debug.Log("Searching from end node, therefore stop");   // todo: remove log
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

        if (neighbour.IsExplored == false && waypointQueue.Contains(neighbour) == false)
        {
            waypointQueue.Enqueue(neighbour);
            neighbour.ExploredFrom = searchCenter;
        }
    }

    private void CreatePath()
    {
        waypointPathList.Add(_endWaypoint);

        WayPoint prevWaypoint = _endWaypoint.ExploredFrom;

        while(prevWaypoint != _startWaypoint)
        {
            waypointPathList.Add(prevWaypoint);
            prevWaypoint = prevWaypoint.ExploredFrom;
        }

        waypointPathList.Add(_startWaypoint);
        waypointPathList.Reverse();
    }
}
