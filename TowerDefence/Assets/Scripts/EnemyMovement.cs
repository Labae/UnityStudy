using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
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
        _followPathWS = new WaitForSeconds(1.0f);

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

        Debug.Log("End Patroll");   // todo : remove log
    }
}
