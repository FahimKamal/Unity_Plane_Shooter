using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float fullHealth = 10f;
    private float _currentHealth = 10f;
    [SerializeField] private HealthBar healthBar;
    
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject explosionSprite;
    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = fullHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.down * (movementSpeed * Time.deltaTime));
    }

    // When bullet hits enemy, enemy takes damage and health bar is updated.
    private void DamageHealthBar(float damage)
    {
        if (!(_currentHealth > 0)) return;
        _currentHealth -= damage;
        healthBar.SetSize(_currentHealth/ fullHealth);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerBullet"))
        {
            DamageHealthBar(col.gameObject.GetComponent<Bullet>().damage);
            Destroy(col.gameObject);
            var damageVfx = Instantiate(damageEffect, col.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            if (_currentHealth <= 0)
            {
                var explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(col.gameObject);
                Destroy(explosion, 0.8f);
            }
        }
    }
}
