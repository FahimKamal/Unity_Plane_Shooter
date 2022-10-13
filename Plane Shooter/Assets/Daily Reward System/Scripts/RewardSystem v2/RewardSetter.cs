using System;
using System.Collections;
using System.Collections.Generic;
using RewardSystemV2;
using UnityEngine;

public class RewardSetter : MonoBehaviour
{
    [SerializeField] private SaveFile gameData;
    [SerializeField] private RewardDatabaseV2 rewardDatabase;
    
    // Max 7 elements
    [SerializeField] private RewardPanel[] rewardPanels;
    
    [SerializeField] private GameObject noInternetPanel;

    [SerializeField] private Sprite metalIcon;
    [SerializeField] private Sprite coinIcon;
    [SerializeField] private Sprite gemIcon;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        //SetRewardPanels(false);
    }

    public void SetRewardPanels(bool isNetworkAvailable)
    {
        if (!isNetworkAvailable)
        {
            Debug.Log("They called me too.");
            noInternetPanel.SetActive(true);
        }
        else
        {
            noInternetPanel.SetActive(false);
            var rewards = rewardDatabase.rewards;
            Sprite icon = null;
            for (var i = 0; i < rewards.Length; i++)
            {
                if (rewards[i].type == RewardType.Metal)
                {
                    icon = metalIcon;
                }
                else if (rewards[i].type == RewardType.Coin)
                {
                    icon = coinIcon;
                }
                else if (rewards[i].type == RewardType.Gem)
                {
                    icon = gemIcon;
                }
                rewardPanels[i].SetRewardPanel(
                    icon, 
                    rewards[i].amount, 
                    rewards[i].isLocked, 
                    rewards[i].isClaimed,
                    i
                );
            }
        }
        
    }
}
