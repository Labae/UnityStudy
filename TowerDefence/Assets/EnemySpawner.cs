using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0f,5f)]
    [SerializeField] private float f_spawnSeconds = 2.0f;
    [SerializeField] private EnemyMovement enemyPrefab;

    private WaitForSeconds ws_spawnSeconds;

	void Start ()
    {
        ws_spawnSeconds = new WaitForSeconds(f_spawnSeconds);

        StartCoroutine(RepeatedlySpawnEnemies());
    }
    
    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, this.gameObject.transform);
            yield return ws_spawnSeconds;
        }
    }
}
