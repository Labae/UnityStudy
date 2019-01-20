using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;
    [SerializeField] private Transform targetEnemy;

    [SerializeField] private float f_attackRange = 30.0f;
    [SerializeField] private ParticleSystem projectileParticle;

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
        ProcessShoot();
    }

    private void ProcessShoot()
    {
        if (targetEnemy)
        {
            objectToPan.LookAt(targetEnemy);
            CheckShootRange();
        }
        else
        {
            ShootCondition(false);
        }
    }

    private void CheckShootRange()
    {
        float distanceEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);

        if(distanceEnemy <= f_attackRange)
        {
            ShootCondition(true);
        }
        else
        {
            ShootCondition(false);
        }
    }

    private void ShootCondition(bool isShoot)
    {
        var emissionModule = projectileParticle.emission;

        emissionModule.enabled = isShoot;
    }
}
