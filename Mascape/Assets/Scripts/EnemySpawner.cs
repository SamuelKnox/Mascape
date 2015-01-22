using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Enemy prefab which will be spawned")]
    public Enemy enemyPrefab;
    [Tooltip("Delay until enemy spawning begins in seconds")]
    public float SpawnDelay = 0.0F;
    [Tooltip("Rate at which enemies spawn in seconds")]
    public float SpawnRate = 1.0F;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", SpawnDelay, SpawnRate);
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as Enemy;
        enemy.transform.parent = transform;
    }
}
