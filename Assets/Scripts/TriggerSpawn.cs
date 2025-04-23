using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] enemySpawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemySpawner();
            Destroy(gameObject);
        }
    }
    void EnemySpawner()
    {
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        }
    }
}