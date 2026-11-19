
using UnityEngine;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Wave Settings")]
    [SerializeField] private int totalWaves = 3;
    [SerializeField] private float delayBetweenWaves = 3f;

    [Header("UI")]
    [SerializeField] private TMP_Text waveText;

    private int currentWave = 0;


    private void Start()
    {
        StartCoroutine(StartWaveSequence());
    }

    private void SetWaveTextPosition(bool center)
    {
        if (waveText == null) return;

        RectTransform rect = waveText.GetComponent<RectTransform>();

        if (center)
        {
            // Posisi tengah layar
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);

            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(800f, 100f);

            waveText.alignment = TextAlignmentOptions.Center;
            waveText.fontSize = 48;
            waveText.textWrappingMode = TextWrappingModes.NoWrap;
        }
        else
        {
            // Posisi kanan atas layar
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);

            rect.anchoredPosition = new Vector2(-10f, 0f);
            rect.sizeDelta = new Vector2(250f, 60f);

            waveText.alignment = TextAlignmentOptions.Right;
            waveText.fontSize = 28;
            waveText.textWrappingMode = TextWrappingModes.NoWrap;
        }
    }

    private IEnumerator StartWaveSequence()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;

            if (waveText != null)
            {
                SetWaveTextPosition(false);
                waveText.text = "WAVE " + currentWave;
            }

            Debug.Log("WAVE " + currentWave + " DIMULAI!");

            int meleeCount = 1 + currentWave;
            int rangedCount = currentWave;

            enemySpawner.SpawnWave(meleeCount, rangedCount);



            while (enemySpawner.AliveEnemyCount > 0)
            {
                yield return new WaitForSeconds(1f);
            }



            Debug.Log("WAVE " + currentWave + " SELESAI!");

            if (waveText != null)
            {
                SetWaveTextPosition(true);
                waveText.text = "WAVE COMPLETED!";
            }

            yield return new WaitForSeconds(2f);

            if (currentWave < totalWaves)
            {
                yield return new WaitForSeconds(delayBetweenWaves);
            }
        }

        if (waveText != null)
        {
            SetWaveTextPosition(true);
            waveText.text = "ALL WAVES COMPLETED!";
        }

        Debug.Log("SEMUA WAVE SELESAI!");
    }

    
}
