using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI endGameCoinText;
    
    private int _coinCount;
    
    public void AddCoin(int coin)
    {
        coinText.text = coin.ToString();
        endGameCoinText.text = "Coins: " + coin;
    }
}
