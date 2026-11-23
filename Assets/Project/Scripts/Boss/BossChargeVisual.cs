
using UnityEngine;

public class BossChargeVisual : MonoBehaviour
{
    [Header("Boss Color")]
    [SerializeField] private SpriteRenderer bossSprite;
    [SerializeField]
    private Color chargeColor =
        new Color(1f, 0.25f, 0.1f, 1f);

    [Header("Aura")]
    [SerializeField] private GameObject redAura;

    private Color originalColor;

    private void Awake()
    {
        if (bossSprite != null)
            originalColor = bossSprite.color;

        if (redAura != null)
            redAura.SetActive(false);
    }

    public void BeginCharge()
    {
        if (bossSprite != null)
            bossSprite.color = chargeColor;

        if (redAura != null)
            redAura.SetActive(true);
    }

    public void EndCharge()
    {
        if (bossSprite != null)
            bossSprite.color = originalColor;

        if (redAura != null)
            redAura.SetActive(false);
    }
}
