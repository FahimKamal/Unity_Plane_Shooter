using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI boxHitText;
    private int hitCount = 0;
    

    private void OnEnable()
    {
        SideEffect.emenyHit += EnemyHit;
    }

    private void OnDisable()
    {
        SideEffect.emenyHit -= EnemyHit;
    }

    private void EnemyHit()
    {
        hitCount++;
        boxHitText.text = "Got hit: "+hitCount;
    }
}
