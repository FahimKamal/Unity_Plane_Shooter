using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameController gameController;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private int enemySpawnCount = 10;
    
    private bool _lastEnemySpawned;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private void Update()
    {
        if (_lastEnemySpawned && FindObjectOfType<EnemyScript>() == null)
        {
            StartCoroutine(gameController.LevelComplete());
        }
    }

    private void SpawnEnemy()
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var randomX = Random.Range(-2, 2);
        Instantiate(enemyPrefabs[enemyIndex], new Vector2(randomX, transform.position.y), Quaternion.identity);
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        for (var i = 0; i < enemySpawnCount; i++)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
        _lastEnemySpawned = true;
    }
}
