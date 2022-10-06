using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private GameObject explosionSprite;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.down * (movementSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerBullet"))
        {
            var explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(col.gameObject);
            Destroy(explosion, 0.8f);
        }
        
    }
}
