using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointPathTracker : MonoBehaviour
{
    [SerializeField] private WayPointPath wayPointPath;
    [SerializeField] private Transform target;

    private Transform[] wayPoints;

    [SerializeField] private bool isLoop = false;

    private int wayPointCounter = 0;
    private int maxWayPointCount;

	void Start ()
    {
        wayPoints = wayPointPath.wayPoints.ToArray();
        maxWayPointCount = wayPointPath.wayPoints.Count;

        target.position = wayPoints[wayPointCounter].position;
    }
    
    void Update ()
    {
        CheckNextWayPoint();
    }

    private void CheckNextWayPoint()
    {
        // 마지막 포인트라면 초기 포인트로 돌아감
        if (wayPointCounter == maxWayPointCount)
        {
            MoveToWayPoint(0);
        }

        if (wayPointCounter < maxWayPointCount)
        {
            if (target.position == wayPoints[wayPointCounter].position)
            {
                wayPointCounter++;
            }
            else
            {
                MoveToWayPoint(wayPointCounter);
            }
        }
        else
        {
            CheckLoop();
        }
    }

    private void CheckLoop()
    {
        if (isLoop == true)
        {
            wayPointCounter = 0;
        }
    }

    private void MoveToWayPoint(int wayPointNumber)
    {
        target.position = Vector3.MoveTowards(target.position, wayPoints[wayPointNumber].position, Time.deltaTime);
    }
}
