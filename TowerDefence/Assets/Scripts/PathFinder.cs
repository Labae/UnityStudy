using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Dictionary<Vector2Int, WayPoint> gridDictionary = new Dictionary<Vector2Int, WayPoint>();

	void Start ()
    {
        LoadBlocks();
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

        Debug.Log(gridDictionary.Count);
    }
}
