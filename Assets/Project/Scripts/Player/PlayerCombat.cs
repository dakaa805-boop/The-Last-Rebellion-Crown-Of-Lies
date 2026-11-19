using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private int infusionBonusDamage = 10;
    [SerializeField] private float attackRange = 0.8f;
    [SerializeField] private float attackCooldown = 0.5f;

    [Header("References")]
    [SerializeField] private PlayerController playerController;

    private float nextAttackTime;
    private bool isInfused;

    // Dipanggil oleh PlayerMagic
    public void SetArcaneInfusion(bool active)
    {
        isInfused = active;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        if (playerController == null)
            return;

        nextAttackTime = Time.time + attackCooldown;

        Vector2 attackPosition =
            (Vector2)transform.position +
            playerController.FacingDirection * attackRange;

        Collider2D[] hitEnemies =
            Physics2D.OverlapCircleAll(attackPosition, 10.5f);
         

        // Debug.Log("Sword detected colliders: " + hitEnemies.Length);

        int totalDamage = attackDamage;

        if (isInfused)
        {
            totalDamage += infusionBonusDamage;
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug.Log("Sword detected: " + enemy.name);

            IDamageable target =
                enemy.GetComponent<IDamageable>();

            if (target == null)
            {
                target = enemy.GetComponentInParent<IDamageable>();
            }

            if (target != null)
            {
                // Debug.Log("IDamageable found: " + enemy.name);
                target.TakeDamage(totalDamage);
            }
            else
            {
                // Debug.LogWarning("IDamageable NOT FOUND: " + enemy.name);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (playerController == null)
            return;

        Vector2 attackPosition =
            (Vector2)transform.position +
            playerController.FacingDirection * attackRange;

        Gizmos.color = isInfused ? Color.cyan : Color.red;
        Gizmos.DrawWireSphere(attackPosition, 10.5f);

    }
}