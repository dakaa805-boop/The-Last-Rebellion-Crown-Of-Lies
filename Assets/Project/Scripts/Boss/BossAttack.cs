
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Heavy Slash Settings")]
    [SerializeField] private int attackDamage = 25;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 2f;

    [Header("Crushing Strike Settings")]
    [SerializeField] private int crushingDamage = 40;
    [SerializeField] private float crushingCooldown = 5f;
    [SerializeField] private float crushingWindup = 1.2f;

    [Header("Ground Slam Settings")]
    [SerializeField] private int groundSlamDamage = 35;
    [SerializeField] private float groundSlamRadius = 2.5f;
    [SerializeField] private float groundSlamCooldown = 8f;
    [SerializeField] private float groundSlamWindup = 1.5f;

    [Header("Heavy Slash Visual")]
    [SerializeField] private GameObject heavySlashEffect;
    [SerializeField] private float slashEffectDuration = 0.2f;
    [SerializeField] private float slashEffectDistance = 0.8f;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Visual References")]
    [SerializeField] private GameObject groundSlamIndicator;

    [Header("Camera Shake")]
    [SerializeField] private CameraShake cameraShake;

    private PlayerHealth playerHealth;
    private BossAI bossAI;
    private BossChargeVisual chargeVisual;

    private float nextAttackTime;
    private float nextCrushingTime;
    private float nextGroundSlamTime;

    private bool isAttacking;


    private void Awake()
    {
        bossAI = GetComponent<BossAI>();
        SetTarget(player);
        chargeVisual = GetComponent<BossChargeVisual>();
        if (groundSlamIndicator != null)
        {
            groundSlamIndicator.transform.localScale = new Vector3(
                5f / transform.lossyScale.x,
                5f / transform.lossyScale.y,
                1f
            );

            groundSlamIndicator.SetActive(false);
        }

        if (heavySlashEffect != null)
            heavySlashEffect.SetActive(false);
    }

    private void Update()
    {
        if (player == null || playerHealth == null)
            return;

        if (isAttacking)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance > attackRange)
            return;

        // Ground Slam
        if (Time.time >= nextGroundSlamTime)
        {
            StartCoroutine(GroundSlam());
            return;
        }

        // Crushing Strike
        if (Time.time >= nextCrushingTime)
        {
            StartCoroutine(CrushingStrike());
            return;
        }

        // Heavy Slash
        if (Time.time >= nextAttackTime)
        {
            HeavySlash();
        }
    }

    private void HeavySlash()
    {
        nextAttackTime = Time.time + attackCooldown;

        playerHealth.TakeDamage(attackDamage);

        if (cameraShake != null)
            cameraShake.Shake(0.12f, 0.04f);

        if (heavySlashEffect != null)
            StartCoroutine(ShowHeavySlashEffect());

        Debug.Log("Boss used Heavy Slash! Damage: " + attackDamage);
    }

    private IEnumerator CrushingStrike()
    {
        isAttacking = true;

        nextCrushingTime = Time.time + crushingCooldown;

        if (bossAI != null)
            bossAI.SetMovementEnabled(false);

        Debug.Log("Boss is preparing Crushing Strike!");

        if (chargeVisual != null)
            chargeVisual.BeginCharge();

        yield return new WaitForSeconds(crushingWindup);

        if (chargeVisual != null)
            chargeVisual.EndCharge();

        if (player != null && playerHealth != null)
        {
            float distance = Vector2.Distance(
                transform.position,
                player.position
            );

            if (distance <= attackRange)
            {
                playerHealth.TakeDamage(crushingDamage);

                if (cameraShake != null)
                    cameraShake.Shake(0.20f, 0.07f);

                Debug.Log("Boss used Crushing Strike!");
            }
            else
            {
                Debug.Log("Crushing Strike missed!");
            }
        }

        FinishSpecialAttack();
    }

    private IEnumerator GroundSlam()
    {
        isAttacking = true;

        nextGroundSlamTime = Time.time + groundSlamCooldown;

        if (bossAI != null)
            bossAI.SetMovementEnabled(false);

        Debug.Log("Boss is preparing Ground Slam!");

        if (groundSlamIndicator != null)
            groundSlamIndicator.SetActive(true);

        yield return new WaitForSeconds(groundSlamWindup);

        if (groundSlamIndicator != null)
            groundSlamIndicator.SetActive(false);

        if (player != null && playerHealth != null)
        {
            float distance = Vector2.Distance(
                transform.position,
                player.position
            );

            if (distance <= groundSlamRadius)
            {
                playerHealth.TakeDamage(groundSlamDamage);

                if (cameraShake != null)
                    cameraShake.Shake(0.30f, 0.12f);

                Debug.Log("Boss used Ground Slam!");
            }
            else
            {
                Debug.Log("Ground Slam missed!");
            }
        }

        FinishSpecialAttack();
    }

    private IEnumerator ShowHeavySlashEffect()
    {
        Vector2 direction =
            (player.position - transform.position).normalized;

        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        heavySlashEffect.transform.position =
            transform.position +
            (Vector3)(direction * slashEffectDistance);

        heavySlashEffect.transform.rotation =
            Quaternion.Euler(0f, 0f, angle);

        heavySlashEffect.SetActive(true);

        yield return new WaitForSeconds(slashEffectDuration);

        heavySlashEffect.SetActive(false);
    }

    private void FinishSpecialAttack()
    {
        isAttacking = false;

        // Beri jeda sebelum serangan berikutnya.
        nextAttackTime = Time.time + attackCooldown;

        if (bossAI != null)
            bossAI.SetMovementEnabled(true);
    }

    public void SetTarget(Transform target)
    {
        player = target;

        playerHealth = player != null
            ? player.GetComponent<PlayerHealth>()
            : null;
    }
}
