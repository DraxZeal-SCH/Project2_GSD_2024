using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawnAreaSize;
    [SerializeField] private Vector2 enemyAreaSize;
    [SerializeField] private int maxEnemiesToSpawn = 5;
    [SerializeField] private float respawnDelay = 5.0f;

    private bool isSpawning = false;
    private string enemyTag = "Enemy";

    private void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnDelay); // Wait before checking for spawn

            if (!isSpawning && CountExistingEnemies() < maxEnemiesToSpawn)
            {
                SpawnEnemies();
            }
        }
    }

    private int CountExistingEnemies()
    {
        Collider2D[] existingEnemies = Physics2D.OverlapBoxAll(transform.position, enemyAreaSize, 0f);
        int taggedEnemiesCount = 0;
        foreach (Collider2D col in existingEnemies)
        {
            if (col.CompareTag(enemyTag))
            {
                taggedEnemiesCount++;
            }
        }
        return taggedEnemiesCount;
    }

    private void SpawnEnemies()
    {
        isSpawning = true;

        int existingEnemiesCount = CountExistingEnemies();
        int enemiesToSpawn = maxEnemiesToSpawn - existingEnemiesCount;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        isSpawning = false;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float spawnX = Random.Range(transform.position.x - spawnAreaSize.x / 2f, transform.position.x + spawnAreaSize.x / 2f);
        float spawnY = Random.Range(transform.position.y - spawnAreaSize.y / 2f, transform.position.y + spawnAreaSize.y / 2f);
        return new Vector2(spawnX, spawnY);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, enemyAreaSize);
    }
}
