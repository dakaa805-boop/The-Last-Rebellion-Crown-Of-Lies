using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangedEnemyAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 7f;
    [SerializeField] private float preferredRange = 5f;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float projectileSpeed = 6f;

    private Rigidbody2D rb;
    private float nextAttackTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float distance = Vector2.Distance(
            rb.position,
            (Vector2)player.position
        );

        if (distance > detectionRange ||
            Mathf.Abs(distance - preferredRange) < 0.3f)
        {
            return;
        }

        Vector2 direction =
            ((Vector2)player.position - rb.position).normalized;

        float movementDirection =
            distance > preferredRange ? 1f : -1f;

        Vector2 nextPosition =
            rb.position + direction * movementDirection *
            moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= detectionRange &&
            distance > preferredRange - 0.5f &&
            distance < preferredRange + 0.5f)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        if (Time.time < nextAttackTime) return;
        if (projectilePrefab == null) return;

        Vector2 direction =
            ((Vector2)player.position -
            (Vector2)transform.position).normalized;

        Vector2 spawnPosition =
            (Vector2)transform.position + direction * 0.5f;

        GameObject projectile = Instantiate(
            projectilePrefab,
            spawnPosition,
            Quaternion.identity
        );

        RangedProjectile projectileScript =
            projectile.GetComponent<RangedProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(
                direction,
                projectileSpeed,
                attackDamage
            );

            nextAttackTime = Time.time + attackCooldown;
            Debug.Log("Ranged Enemy menembakkan proyektil!");
        }
        else
        {
            Debug.LogError("RangedProjectile script tidak ditemukan!");
            Destroy(projectile);
        }
    }
}