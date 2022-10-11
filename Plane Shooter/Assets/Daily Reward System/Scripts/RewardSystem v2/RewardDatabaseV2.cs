using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RewardSystemV2
{
    public enum RewardType
    {
        Metal,
        Coin,
        Gem,
    }
    
    [Serializable]
    public struct Reward
    {
        public RewardType type;
        public int amount;
        [HideInInspector] public bool isClaimed;
        [HideInInspector] public bool isLocked;
    }
    
    [CreateAssetMenu(fileName = "RewardsDBV2", menuName = "Daily Rewards System/Rewards DatabaseV2")]
    public class RewardDatabaseV2 : ScriptableObject
    {
        public int rewardCount = 7;
        public Reward[] rewards;

        public Reward GetReward(int index)
        {
            return rewards[index];
        }
    }
}

