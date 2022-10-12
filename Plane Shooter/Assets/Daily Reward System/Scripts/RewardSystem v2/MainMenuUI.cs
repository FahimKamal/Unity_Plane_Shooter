using System.Collections;
using System.Collections.Generic;
using RewardSystemV2;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI elements")] [Space]
    [SerializeField] private TextMeshProUGUI metalText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    
    [Space]
    [Header("Database")] [Space]
    [SerializeField] private SaveFile gameData;
    // Start is called before the first frame update
    void Start()
    {
        metalText.text = gameData.totalMetals.ToString();
        coinText.text = gameData.totalCoins.ToString();
        gemText.text = gameData.totalGems.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
