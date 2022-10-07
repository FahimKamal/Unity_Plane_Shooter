using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float respawnTime = 2f;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }
    
    private void SpawnEnemy()
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var randomX = Random.Range(-2, 2);
        Instantiate(enemyPrefabs[enemyIndex], new Vector2(randomX, transform.position.y), Quaternion.identity);
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemy();
        }
    }
}
