using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform enemySpawn;
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, enemySpawn);
    }
}
