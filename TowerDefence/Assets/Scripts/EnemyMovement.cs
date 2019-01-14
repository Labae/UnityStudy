using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Tooltip("Parents Name must be World")]
    [SerializeField] private GameObject world;
    [SerializeField] private List<WayPoint> path;

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
    }

	void Start ()
    {
        _followPathWS = new WaitForSeconds(1.0f);

        StartCoroutine(FollowPath());
	}

    private IEnumerator FollowPath()
    {
        foreach(WayPoint wayPoint in path)
        {
            transform.position = wayPoint.transform.position;
            yield return _followPathWS;
        }
    }
}
