using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Collider collisionMesh;
    [SerializeField] private int i_hitPoints;
    
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("I'm hit");
        ProcessHit();

        if(i_hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        i_hitPoints = i_hitPoints - 1;
    }

    private void KillEnemy()
    {
        Destroy(this.gameObject);
    }
}
