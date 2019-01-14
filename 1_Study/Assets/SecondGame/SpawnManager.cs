using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Hero _hero;

    [SerializeField] private Transform _heroSpawnTransform;

	void Start ()
    {
        _hero = GameObject.FindObjectOfType<Hero>();

        if (_heroSpawnTransform != null)
        {
            _hero.transform.position = _heroSpawnTransform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
