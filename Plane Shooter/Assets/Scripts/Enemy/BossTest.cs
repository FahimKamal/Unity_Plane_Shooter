using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ouhc");
        if(collision.CompareTag("Bullet"))
        {
            SideEffect.emenyHit?.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
