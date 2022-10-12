using System;
using RewardSystemV2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [Header("UI elements")] [Space]
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    
    [Space] [Header("Reward Panel items")] [Space]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject lockedPanel;
    [SerializeField] private GameObject collectedPanel;
    [SerializeField] private Button collectButton;
    
    [Space]
    [Header("Database")] [Space]
    [SerializeField] private RewardDatabaseV2 rewardDatabase;
    [SerializeField] private SaveFile gameData;
    
    public int rewardIndex;
    
    public void SetRewardPanel(Sprite iconSprite, int amount, bool isLocked, bool isCollected, int index)
    {
        icon.sprite = iconSprite;
        amountText.text = amount.ToString();
        lockedPanel.SetActive(isLocked);
        collectedPanel.SetActive(isCollected);
        collectButton.interactable = !isCollected && !isLocked;
        rewardIndex = index;
    }

    private void Start()
    {
        collectButton.onClick.RemoveAllListeners();
        collectButton.onClick.AddListener(OnCollectButtonClicked);
    }

    private void OnCollectButtonClicked()
    {
        var reward = rewardDatabase.GetReward(rewardIndex);
        reward.isClaimed = true;
        
        switch (reward.type)
        {
            case RewardType.Metal:
                gameData.totalMetals += reward.amount;
                metalText.text = gameData.totalMetals.ToString();
                break;
            case RewardType.Coin:
                gameData.totalCoins += reward.amount;
                coinText.text = gameData.totalCoins.ToString();
                break;
            case RewardType.Gem:
                gameData.totalGems += reward.amount;
                gemText.text = gameData.totalGems.ToString();
                break;
        }
        
        // Unlock next reward
        if (rewardIndex < rewardDatabase.rewards.Length - 1)
        {
            rewardDatabase.rewards[rewardIndex + 1].isLocked = false;
            gameData.loginCount = rewardIndex + 1;
        }
        else
        {
            gameData.loginCount = 0;
        }

        collectedPanel.SetActive(true);
        collectButton.interactable = false;
    }


}
