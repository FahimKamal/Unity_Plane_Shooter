using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RewardSystemV2
{
    [CreateAssetMenu(fileName = "SaveGameData", menuName = "Daily Rewards System/SaveGameData")]
    public class SaveFile : ScriptableObject
    {
        // Day 1 to day 7
        public int loginCount;
        
        public int totalMetals;
        public int totalCoins;
        public int totalGems;
    }
}

