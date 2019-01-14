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

        _hero.transform.position = _heroSpawnTransform.position;
    }
}
