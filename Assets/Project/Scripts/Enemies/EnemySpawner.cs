
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Player Target")]
    [SerializeField] private Transform player;

    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private GameObject rangedPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;



    private readonly List<GameObject> spawnedEnemies =
        new List<GameObject>();

    public int AliveEnemyCount
    {
        get
        {
            spawnedEnemies.RemoveAll(enemy => enemy == null);
            return spawnedEnemies.Count;
        }
    }

    public void SpawnWave(int meleeCount, int rangedCount)
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Spawn Points belum diatur!");
            return;
        }

        if (meleePrefab == null || rangedPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab belum diatur!");
            return;
        }

        spawnedEnemies.Clear();

        SpawnEnemyType(meleePrefab, meleeCount);
        SpawnEnemyType(rangedPrefab, rangedCount);

        Debug.Log("Wave berhasil di-spawn!");
    }

    private void SpawnEnemyType(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint =
                spawnPoints[spawnedEnemies.Count % spawnPoints.Length];

            GameObject enemy = Instantiate(
                prefab,
                spawnPoint.position,
                Quaternion.identity
            );

            EnemyAI meleeAI = enemy.GetComponent<EnemyAI>();

            if (meleeAI != null)
            {
                meleeAI.SetTarget(player);
            }

            RangedEnemyAI rangedAI = enemy.GetComponent<RangedEnemyAI>();

            if (rangedAI != null)
            {
                rangedAI.SetTarget(player);
            }

            spawnedEnemies.Add(enemy);
        }
    }
}
