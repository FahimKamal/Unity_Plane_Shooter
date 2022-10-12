using System;
using System.Collections;
using System.Collections.Generic;
using RewardSystemV2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private float nextRewardDelayTime = 20f;
    
    [Header("UI elements")] [Space]
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    
    [SerializeField] private Button openRewardButton;
    [SerializeField] private Button closeRewardButton;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private GameObject rewardNotification;
    
    [SerializeField] private RewardSetter rewardSetter;
    
    [Space]
    [Header("Database")] [Space]
    [SerializeField] private SaveFile gameData;
    [SerializeField] private RewardDatabaseV2 rewardDatabase;
    
    // Start is called before the first frame update
    void Start()
    {
        metalText.text = gameData.totalMetals.ToString();
        coinText.text = gameData.totalCoins.ToString();
        gemText.text = gameData.totalGems.ToString();
        
        openRewardButton.onClick.RemoveAllListeners();
        openRewardButton.onClick.AddListener(OpenRewardPanel);
        
        closeRewardButton.onClick.RemoveAllListeners();
        closeRewardButton.onClick.AddListener(CloseRewardPanel);
        
        InvokeRepeating(nameof(CheckReward), 0f, 5f);
    }

    private void CheckReward()
    {
        if (gameData.lastLoginDate is "" or "0")
        {
            gameData.lastLoginDate = DateTime.Now.ToString();
            FirstLogin();
            rewardNotification.SetActive(true);
            return;
        }
        
        var currentDate = DateTime.Now;
        var lastLoginDate = DateTime.Parse(gameData.lastLoginDate);
        var totalSeconds = (currentDate - lastLoginDate).TotalSeconds;

        if (totalSeconds >= nextRewardDelayTime)
        {
            var rewards = rewardDatabase.rewards;
            for (int i = 0; i < rewards.Length; i++)
            {
                if (i == gameData.loginCount)
                {
                    rewards[i].isLocked = false;
                    rewardNotification.SetActive(true);
                    rewardSetter.SetRewardPanels();
                    break;
                }
            }
        }
    }
    
    private void FirstLogin()
    {
        var rewards = rewardDatabase.rewards;
        for (var i = 0; i < rewards.Length; i++)
        {
            rewards[i].isLocked = true;
            rewards[i].isClaimed = false;
        }
        rewards[0].isLocked = false;
        rewardSetter.SetRewardPanels();
    }
    
    private void OpenRewardPanel()
    {
        rewardPanel.SetActive(true);
        openRewardButton.interactable = false;
    }
    
    private void CloseRewardPanel()
    {
        rewardPanel.SetActive(false);
        openRewardButton.interactable = true;
    }
}
