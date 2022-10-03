using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        SideEffect.emenyHit?.Invoke();
        
        Debug.Log("ouhc");
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
