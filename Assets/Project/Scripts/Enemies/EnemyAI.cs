using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Attack")]
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1.5f;

    private Rigidbody2D rb;
    private float nextAttackTime;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(
            rb.position,
            (Vector2)player.position
        );

        if (distance > detectionRange ||
            distance <= attackRange)
        {
            return;
        }

        Vector2 direction =
            ((Vector2)player.position - rb.position).normalized;

        Vector2 nextPosition =
            rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= attackRange)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (Time.time < nextAttackTime)
            return;

        PlayerHealth health =
            player.GetComponent<PlayerHealth>();

        if (health == null)
        {
            health = player.GetComponentInParent<PlayerHealth>();
        }

        if (health == null)
            return;

        nextAttackTime = Time.time + attackCooldown;
        AttackFeedback();
        health.TakeDamage(attackDamage);
        Debug.Log("Enemy menyerang Player!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void AttackFeedback()
    {  
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;

            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 0.15f);
        }
    }

    private void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

}