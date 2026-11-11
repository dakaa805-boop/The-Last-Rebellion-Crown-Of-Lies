using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [Header("Mana Settings")]
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana = 100f;
    [SerializeField] private float manaRegen = 5f;

    [Header("Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballCost = 20f;
    [SerializeField] private float fireballCooldown = 1.5f;
    [SerializeField] private float fireballSpawnDistance = 0.7f;

    [Header("Arcane Infusion")]
    [SerializeField] private float infusionCost = 30f;
    [SerializeField] private float infusionDuration = 5f;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerCombat playerCombat;

    private float nextFireballTime;
    private float infusionEndTime;

    public float CurrentMana => currentMana;
    public float MaxMana => maxMana;

    public bool IsInfused => Time.time < infusionEndTime;

    private void Update()
    {
        //Debug.Log("PlayerMagic is running");

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F KEY DETECTED!");
            CastFireball();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateInfusion();
        }

        RegenerateMana();
    }

    private void RegenerateMana()
    {
        currentMana = Mathf.Min(
            currentMana + manaRegen * Time.deltaTime,
            maxMana
        );
    }

    private bool UseMana(float amount)
    {
        if (currentMana < amount)
        {
            Debug.Log("Not enough mana!");
            return false;
        }

        currentMana -= amount;
        return true;
    }

    private void CastFireball()
    {
        Debug.Log("1. CastFireball called!");

        if (Time.time < nextFireballTime)
        {
            Debug.Log("2. Fireball masih cooldown!");
            return;
        }

        if (fireballPrefab == null)
        {
            Debug.LogError("3. Fireball Prefab belum dipasang!");
            return;
        }

        if (playerController == null)
        {
            Debug.LogError("4. PlayerController belum dipasang!");
            return;
        }

        if (!UseMana(fireballCost))
        {
            Debug.Log("5. Mana tidak cukup!");
            return;
        }

        nextFireballTime = Time.time + fireballCooldown;

        Vector2 direction = playerController.FacingDirection;

        Vector3 spawnPosition =
            transform.position +
            (Vector3)(direction * fireballSpawnDistance);

        Debug.Log("6. Spawn position: " + spawnPosition);

        GameObject projectile = Instantiate(
            fireballPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Debug.Log("7. Fireball berhasil dibuat!");

        Fireball fireball = projectile.GetComponent<Fireball>();

        if (fireball != null)
        {
            fireball.Initialize(direction);
            Debug.Log("8. Fireball initialized!");
        }
        else
        {
        Debug.LogError("9. Fireball script tidak ditemukan pada Prefab!");
        }
    }

    private void ActivateInfusion()
    {
        if (IsInfused)
            return;

        if (playerCombat == null)
            return;

        if (!UseMana(infusionCost))
            return;

        infusionEndTime =
            Time.time + infusionDuration;

        playerCombat.SetArcaneInfusion(true);

        Debug.Log("Arcane Infusion activated!");
    }

    private void LateUpdate()
    {
        if (!IsInfused && playerCombat != null)
        {
            playerCombat.SetArcaneInfusion(false);
        }
    }
}