
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image healthFill;
    [SerializeField] private TMP_Text bossName;

    public void SetBossName(string name)
    {
        bossName.text = name;
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (maxHealth <= 0) return;

        healthFill.fillAmount = currentHealth / maxHealth;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
