using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float f_moveToSecond = 2.0f;

    [Tooltip("Parents Name must be World")]
    [SerializeField] private GameObject world;
    [SerializeField] private List<WayPoint> path;
    
    private PathFinder pathFinder;

    // Timer;
    private WaitForSeconds _followPathWS;

    public List<WayPoint> Path
    {
        get { return path; }
        set { path = value; }
    }

    public GameObject World
    {
        get { return world; }
        set { world = value; }
    }

	void Start ()
    {
        _followPathWS = new WaitForSeconds(f_moveToSecond);

        pathFinder = FindObjectOfType<PathFinder>();

        List<WayPoint> path = pathFinder.GetWaypointPathList();

        StartCoroutine(FollowPath(path));
	}

    private IEnumerator FollowPath(List<WayPoint> path)
    {
        foreach(WayPoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;
            yield return _followPathWS;
        }
    }
}
