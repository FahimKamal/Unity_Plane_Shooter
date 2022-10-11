using System.Collections;
using System.Collections.Generic;
using DailyRewardSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsDB", menuName = "Daily Rewards System/Rewards Database")]
public class RewardsDatabase : ScriptableObject
{
    public Reward[] rewards;
    
    public int RewardsCount => rewards.Length;
    
    public Reward GetReward(int index) { return rewards[index]; }
}
 