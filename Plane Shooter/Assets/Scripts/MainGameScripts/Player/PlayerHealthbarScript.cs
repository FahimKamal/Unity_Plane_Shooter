using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbarScript : MonoBehaviour
{
    [SerializeField] private Image bar;
    
    public void SetHealth(float health)
    {
        bar.fillAmount = health;
    }
    
}
