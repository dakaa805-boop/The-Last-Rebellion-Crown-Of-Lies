using UnityEngine;

public class BossEntranceTrigger : MonoBehaviour
{
    [SerializeField] private BossSpawner bossSpawner;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger mendeteksi: " + other.name);

        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player terdeteksi!");

            hasTriggered = true;

            if (bossSpawner == null)
            {
                Debug.LogError("BossSpawner belum terhubung!");
                return;
            }

            bossSpawner.SpawnBoss();
        }
    }
}