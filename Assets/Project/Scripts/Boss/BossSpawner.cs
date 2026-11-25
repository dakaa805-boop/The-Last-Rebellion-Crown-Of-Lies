
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Boss References")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform player;
 

    [Header("Boss UI")]
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private GameObject bossVictoryPanel;

    [Header("Boss Reward")]
    [SerializeField] private int bossGoldReward = 100;

    private GameObject spawnedBoss;

    private void Start()
    {
        if (bossVictoryPanel != null)
            bossVictoryPanel.SetActive(false);

        if (bossHealthBar != null)
            bossHealthBar.Hide();
    }

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

        if (bossHealth != null)
        {
            Debug.Log("EnemyHealth found!");

            if (bossHealthBar != null)
            {
                bossHealth.SetBossHealthBar(bossHealthBar);
                bossHealthBar.Show();
                Debug.Log("Health Bar assigned!");
            }

            if (bossVictoryPanel != null)
            {
                Debug.Log("Sending panel: " + bossVictoryPanel.name);

                bossHealth.SetBossVictoryPanel(bossVictoryPanel);

                Debug.Log("Victory Panel sent!");
            }
            else
            {
                Debug.LogError("BossSpawner Victory Panel is NULL!");
            }

            PlayerCurrency currency = player.GetComponent<PlayerCurrency>();

            if (currency != null)
            {
                bossHealth.SetBossReward(currency, bossGoldReward);
            }
        }
        else
        {
            Debug.LogError("EnemyHealth component not found on Boss!");
        }
    }
}
