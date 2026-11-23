
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Boss References")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform player;

    [Header("Boss UI")]
    [SerializeField] private BossHealthBar bossHealthBar;

    private GameObject spawnedBoss;
    
    public void SpawnBoss()
    {
        if (spawnedBoss != null) return;

        if (bossPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Boss Prefab atau Spawn Point belum diisi!");
            return;
        }

        spawnedBoss = Instantiate(
            bossPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        BossAI bossAI = spawnedBoss.GetComponent<BossAI>();

        if (bossAI != null && player != null)
        {
            bossAI.SetTarget(player);
        }

        BossAttack bossAttack = spawnedBoss.GetComponent<BossAttack>();

        if (bossAttack != null && player != null)
        {
            bossAttack.SetTarget(player);
        }

        EnemyHealth bossHealth = spawnedBoss.GetComponent<EnemyHealth>();

        if (bossHealth != null && bossHealthBar != null)
        {
            bossHealth.SetBossHealthBar(bossHealthBar);
        }
        
        if (bossHealthBar != null)
        {
            bossHealthBar.Show();
        }
        
        Debug.Log("Boss berhasil muncul!");
    }
}
