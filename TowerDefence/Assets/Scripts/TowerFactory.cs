using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private int towerLimit = 5;
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private Transform towerParent;

    private Queue<Tower> towerQueue = new Queue<Tower>();
    
    public void AddTower(WayPoint baseWaypoint)
    {
        int numTowers = towerQueue.Count;

        if(numTowers < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(WayPoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity, towerParent);
        baseWaypoint.IsPlaceable = false;

        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(WayPoint baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();

        towerQueue.Enqueue(oldTower);
    }
}
