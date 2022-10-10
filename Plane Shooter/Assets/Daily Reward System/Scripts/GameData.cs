using UnityEngine;

namespace Daily_Reward_System.Scripts
{
    public static class GameData
    {
        private static int _metals = 0;
        private static int _coins = 0;
        private static int _gems = 0;

        static GameData()
        {
            Metals = PlayerPrefs.GetInt("Metals", 0);
            Coins = PlayerPrefs.GetInt("Coins", 0);
            Gems = PlayerPrefs.GetInt("Gems", 0);
        }
        
        public static int Metals
        {
            get => _metals;
            set
            {
                _metals = value;
                PlayerPrefs.SetInt("Metals", value);
            }
        }
        
        public static int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                PlayerPrefs.SetInt("Coins", value);
            }
        }
        
        public static int Gems
        {
            get => _gems;
            set
            {
                _gems = value;
                PlayerPrefs.SetInt("Gems", value);
            }
        }
        
    }
}
