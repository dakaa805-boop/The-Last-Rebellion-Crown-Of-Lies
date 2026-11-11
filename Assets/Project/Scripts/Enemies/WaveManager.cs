
using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Wave Settings")]
    [SerializeField] private int totalWaves = 3;
    [SerializeField] private float delayBetweenWaves = 3f;

    private int currentWave = 0;
    private bool waveRunning = false;

    private void Start()
    {
        StartCoroutine(StartWaveSequence());
    }

    private IEnumerator StartWaveSequence()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;

            Debug.Log("WAVE " + currentWave + " DIMULAI!");

            int meleeCount = 1 + currentWave;
            int rangedCount = currentWave;

            enemySpawner.SpawnWave(meleeCount, rangedCount);

            waveRunning = true;

            while (enemySpawner.AliveEnemyCount > 0)
            {
                yield return new WaitForSeconds(1f);
            }

            waveRunning = false;

            Debug.Log("WAVE " + currentWave + " SELESAI!");

            if (currentWave < totalWaves)
                yield return new WaitForSeconds(delayBetweenWaves);
        }

        Debug.Log("SEMUA WAVE SELESAI!");
    }
}
