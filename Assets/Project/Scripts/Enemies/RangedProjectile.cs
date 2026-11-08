using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangedProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float lifetime = 4f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;
    private int damage;
    private bool initialized;
    private bool hasHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(
        Vector2 shootDirection,
        float projectileSpeed,
        int projectileDamage)
    {
        direction = shootDirection.normalized;
        speed = projectileSpeed;
        damage = projectileDamage;
        initialized = true;
    }

    private void FixedUpdate()
    {
        if (!initialized || hasHit) return;

        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health == null)
            health = other.GetComponentInParent<PlayerHealth>();

        if (health != null)
        {
            hasHit = true;
            rb.linearVelocity = Vector2.zero;
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}