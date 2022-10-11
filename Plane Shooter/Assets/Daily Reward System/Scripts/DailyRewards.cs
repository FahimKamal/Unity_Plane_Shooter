using System;
using System.Collections;
using Daily_Reward_System.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DailyRewardSystem
{
    public enum RewardType
    {
        Metals,
        Coins,
        Gems,
    }

    [Serializable]
    public struct Reward
    {
        public RewardType type;
        public int amount;
    }

    public class DailyRewards : MonoBehaviour
    {
        [Header("Main Menu UI")] 
        [SerializeField] private TextMeshProUGUI metalsText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI gemsText;

        [Space] [Header("Daily Reward UI")] 
        [SerializeField] private GameObject rewardCanvas;

        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardAmountText;
        [SerializeField] private Button claimButton;
        [SerializeField] private GameObject rewardNotificationImage;
        [SerializeField] private GameObject noMoreRewardsPanel;
        
        [Space] [Header("Reward Sprites")]
        [SerializeField] private Sprite metalSprite;
        [SerializeField] private Sprite coinSprite;
        [SerializeField] private Sprite gemSprite;

        [Space] [Header("Reward Particle Effects")] 
        [SerializeField] private ParticleSystem fxMetals;
        [SerializeField] private ParticleSystem fxCoins;
        [SerializeField] private ParticleSystem fxGems;
        

        [Space] [Header("Rewards DataBase")] [SerializeField]
        private RewardsDatabase rewardsDatabase;
        
        [SerializeField] private float nextRewardDelayTime = 10f;
        
        // Check for rewards every 5 seconds.
        [SerializeField] private float rewardCheckDelayTime = 5f;

        private int _nextRewardIndex;
        private bool _isRewardAvailable;

        // Start is called before the first frame update
        private void Start()
        {
            PlayerPrefs.DeleteAll();
            Initialize();
            StartCoroutine(CheckForRewards());
        }

        private void Initialize()
        {
            _nextRewardIndex = PlayerPrefs.GetInt("NextRewardIndex", 0);

            UpdateMetalTextUI();
            UpdateCoinTextUI();
            UpdateGemsTextUI();

            // Add Click Events
            openButton.onClick.RemoveAllListeners();
            openButton.onClick.AddListener(OnOpenButtonClick);

            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(OnCloseButtonClick);

            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(OnClaimButtonClick);
            
            // Check if the game is opened for the first time, then set LastClaimTime to current time
            if (!PlayerPrefs.HasKey("LastClaimTime"))
            {
                PlayerPrefs.SetString("LastClaimTime", DateTime.Now.ToString());
            }
        }


        private IEnumerator CheckForRewards()
        {
            while (true)
            {
                if (!_isRewardAvailable)
                {
                    var currentTime = DateTime.Now;
                    var lastClaimTime = DateTime.Parse(PlayerPrefs.GetString("LastClaimTime"));
            
                    // Get total seconds between this 2 dates
                    var totalSeconds = (currentTime - lastClaimTime).TotalSeconds;
            
                    // If the total seconds is greater than the next reward delay time, then activate the reward.
                    if (totalSeconds >= nextRewardDelayTime)
                    {
                        ActiveReward();
                    }
                    else
                    {
                        DeactivateReward();
                    }
                }

                yield return new WaitForSeconds(rewardCheckDelayTime);
            }
        }

        void ActiveReward()
        {
            _isRewardAvailable = true;
            
            noMoreRewardsPanel.SetActive(false);
            rewardNotificationImage.SetActive(true);
            
            // Update Reward UI
            var reward = rewardsDatabase.GetReward(_nextRewardIndex);
            
            if (reward.type == RewardType.Metals){
                rewardImage.sprite = metalSprite;
            }
            else if (reward.type == RewardType.Coins){
                rewardImage.sprite = coinSprite;
            }
            else { // reward.type == RewardType.Gems
                rewardImage.sprite = gemSprite;
            }
            
            rewardAmountText.text = $"+{reward.amount.ToString()}";
        }

        private void DeactivateReward()
        {
            _isRewardAvailable = false;
            
            noMoreRewardsPanel.SetActive(true);
            rewardNotificationImage.SetActive(false);
        }

        // Update Main Menu UI (Metals, Coins, Gems)
        void UpdateMetalTextUI()
        {
            metalsText.text = GameData.Metals.ToString();
        }

        void UpdateCoinTextUI()
        {
            coinText.SetText(GameData.Coins.ToString());
        }

        void UpdateGemsTextUI()
        {
            gemsText.SetText(GameData.Gems.ToString());
        }

        // Open | Close Daily Reward UI
        void OnOpenButtonClick()
        {
            rewardCanvas.SetActive(true);
        }

        void OnCloseButtonClick()
        {
            rewardCanvas.SetActive(false);
        }

        void OnClaimButtonClick()
        {
            var reward = rewardsDatabase.GetReward(_nextRewardIndex);

            // Check Reward Type
            if (reward.type == RewardType.Metals)
            {
                Debug.Log("<color=white>" + reward.type.ToString() + "Claimed : </color>+" + reward.amount);
                GameData.Metals += reward.amount;
                fxMetals.Play();
                UpdateMetalTextUI();
            }
            else if (reward.type == RewardType.Coins)
            {
                Debug.Log("<color=yellow>" + reward.type.ToString() + "Claimed : </color>+" + reward.amount);
                GameData.Coins += reward.amount;
                fxCoins.Play();
                UpdateCoinTextUI();
            }
            else{ // RewardType.Type == RewardType.Gems
                Debug.Log("<color=green>" + reward.type.ToString() + "Claimed : </color>+" + reward.amount);
                GameData.Gems += reward.amount;
                fxGems.Play();
                UpdateGemsTextUI();
            }
            
            // Save Next Reward Index
            _nextRewardIndex++;
            if (_nextRewardIndex >= rewardsDatabase.RewardsCount)
            {
                _nextRewardIndex = 0;
            }
            PlayerPrefs.SetInt("NextRewardIndex", _nextRewardIndex);
            
            // Save Datetime of the last claim click
            var lastClaimTime = DateTime.Now;
            PlayerPrefs.SetString("LastClaimTime", lastClaimTime.ToString());
            
            DeactivateReward();
        }
    }
}