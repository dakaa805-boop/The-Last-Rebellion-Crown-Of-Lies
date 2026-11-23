
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossAI : MonoBehaviour
{
    [Header("Boss Movement")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("References")]
    [SerializeField] private Transform player;

    private Rigidbody2D rb;
    private bool movementEnabled = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!movementEnabled)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (player == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            (player.position - transform.position).normalized;

        float distance =
            Vector2.Distance(transform.position, player.position);

        if (distance > detectionRange || distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = direction * moveSpeed;
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;

        if (!enabled)
            rb.linearVelocity = Vector2.zero;
    }
}
