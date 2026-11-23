using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;

    [Header("Boss Settings")]
    [SerializeField] private bool isBoss = false;
    [SerializeField] private BossHealthBar bossHealthBar;

    private int currentHealth;
    private DamageFlash damageFlash;
    private SpriteRenderer spriteRenderer;
    private EnemyAI enemyAI;
    private Collider2D enemyCollider;
    private Rigidbody2D rb;

    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (isBoss && bossHealthBar != null)
        {
            bossHealthBar.SetHealth(currentHealth, maxHealth);

            Debug.Log("Boss UI Updated: " + currentHealth + "/" + maxHealth);
        }
        else
        {
            Debug.LogWarning(
                "Boss UI tidak terhubung! isBoss: " + isBoss
            );
        }

        damageFlash = GetComponent<DamageFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

public void SetBossHealthBar(BossHealthBar healthBar)
{
    if (!isBoss) return;

    bossHealthBar = healthBar;

    if (bossHealthBar != null)
    {
        bossHealthBar.SetBossName(
            gameObject.name.Replace("(Clone)", "").Trim()
        );

        bossHealthBar.SetHealth(currentHealth, maxHealth);
    }
}

public void TakeDamage(int damage)
{
    currentHealth -= damage;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    if (damageFlash != null)
        damageFlash.Flash();

    if (isBoss && bossHealthBar != null)
        bossHealthBar.SetHealth(currentHealth, maxHealth);

    Debug.Log(gameObject.name + " menerima " + damage +
              " damage. HP: " + currentHealth);

    if (currentHealth <= 0)
        Die();
}

    private void Die()
{
    if (isDead) return;
    isDead = true;

    if (isBoss && bossHealthBar != null)
        bossHealthBar.Hide();

    if (enemyAI != null)
        enemyAI.enabled = false;

    if (enemyCollider != null)
        enemyCollider.enabled = false;

    if (rb != null)
        rb.linearVelocity = Vector2.zero;

    StartCoroutine(DeathRoutine());
}

    private IEnumerator DeathRoutine()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}