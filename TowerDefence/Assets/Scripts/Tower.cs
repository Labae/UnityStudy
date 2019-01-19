using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;
    [SerializeField] private Transform targetEnemy;

    private void Start()
    {
        CheckNullValue();
    }

    private void CheckNullValue()
    {
        if (objectToPan == null)
        {
            SetObjectToPan();
        }

        if (targetEnemy == null)
        {
            SetTargetEnemy();
        }
    }

    private void SetObjectToPan()
    {
        objectToPan = transform.GetChild(0);
    }

    private void SetTargetEnemy()
    {
        targetEnemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    void Update ()
    {
        objectToPan.LookAt(targetEnemy);
	}
}
