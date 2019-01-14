using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Parents Name must be World")]
    [SerializeField] private GameObject world;
    [SerializeField] private List<Block> path;

    public List<Block> Path
    {
        get { return path; }
        set { path = value; }
    }

    public GameObject World
    {
        get { return world; }
    }

	void Start ()
    {
        PrintAllWayPoints();
	}

    private void PrintAllWayPoints()
    {
        foreach(Block wayPoint in path)
        {
            print(wayPoint.name);
        }
    }

    void Update ()
    {
		
	}
}
