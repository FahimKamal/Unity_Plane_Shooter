using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    
    public void AddCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
