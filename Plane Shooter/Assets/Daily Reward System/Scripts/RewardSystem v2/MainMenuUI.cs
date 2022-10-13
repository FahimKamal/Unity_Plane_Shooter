using System;
using System.Collections;
using System.Collections.Generic;
using RewardSystemV2;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private WorldTimeAPI worldTimeAPI;
    [SerializeField] private float nextRewardDelayTime = 20f;
    [SerializeField] private bool isNetworkAvailable = false;
    
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
        if (worldTimeAPI.isTimeLoaded)
        {
            Debug.Log("Check check");
            var currentDate = worldTimeAPI.GetCurrentDateTime(out isNetworkAvailable);
            if (gameData.lastLoginDate is "" or "0")
            {
                gameData.lastLoginDate = currentDate.ToString();
                FirstLogin();
                rewardNotification.SetActive(true);
                return;
            }

            var lastLoginDate = DateTime.Parse(gameData.lastLoginDate);

            var totalSeconds = (currentDate - lastLoginDate).TotalSeconds;

            if (totalSeconds >= nextRewardDelayTime)
            {
                var rewards = rewardDatabase.rewards;
                for (var i = 0; i < rewards.Length; i++)
                {
                    if (i == gameData.loginCount)
                    {
                        rewards[i].isLocked = false;
                        rewardNotification.SetActive(true);
                        rewardSetter.SetRewardPanels(true);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Some one is calling me");
            rewardSetter.SetRewardPanels(false);
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
        rewardSetter.SetRewardPanels(true);
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
