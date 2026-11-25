
using UnityEngine;
using TMPro;

public class PlayerCurrency : MonoBehaviour
{
    [Header("Currency")]
    [SerializeField] private int startingGold = 0;

    [Header("UI")]
    [SerializeField] private TMP_Text goldText;

    private int currentGold;

    private void Awake()
    {
        currentGold = startingGold;
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        currentGold += amount;

        UpdateGoldUI();

        Debug.Log("Gold received: +" + amount);
        Debug.Log("Current Gold: " + currentGold);
    }

    public int GetGold()
    {
        return currentGold;
    }

    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "GOLD: " + currentGold;
        }
    }
}
