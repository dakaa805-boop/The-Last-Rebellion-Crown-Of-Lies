using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 30;

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

        damageFlash = GetComponent<DamageFlash>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyAI = GetComponent<EnemyAI>();
        enemyCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (damageFlash != null)
        {
            damageFlash.Flash();
        }

        Debug.Log(gameObject.name + " HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

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