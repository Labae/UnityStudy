using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform objectToPan;

    private Transform targetEnemy;

    [SerializeField] private float f_attackRange = 30.0f;
    [SerializeField] private ParticleSystem projectileParticle;

    public WayPoint baseWaypoint;

    private void Start()
    {
        FindPan();
    }

    private void FindPan()
    {
        if(objectToPan == null)
        {
            objectToPan = transform.GetChild(0);
        }
    }

    void Update ()
    {
        SetTargetEnemy();
        ProcessShoot();
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage enemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if(distToA <= distToB)
        {
            return transformA;
        }
        else
        {
            return transformB;
        }
    }

    private void ProcessShoot()
    {
        if (targetEnemy != null)
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
