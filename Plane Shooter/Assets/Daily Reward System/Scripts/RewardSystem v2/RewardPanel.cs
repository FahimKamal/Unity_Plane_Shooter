using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject lockedPanel;
    [SerializeField] private GameObject collectedPanel;
    
    public void SetRewardPanel(Sprite iconSprite, int amount, bool isLocked, bool isCollected)
    {
        this.icon.sprite = iconSprite;
        amountText.text = amount.ToString();
        lockedPanel.SetActive(isLocked);
        collectedPanel.SetActive(isCollected);
    }
}
