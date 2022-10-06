using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float fullHealth = 100f;
    private float _currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerHealthbarScript healthBarUIScript;
    [SerializeField] private GameObject particleBlast;
    
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float padding = 0.8f;
    
    // Private variables to store the screen boundaries.
    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;
    
    // Start is called before the first frame update
    private void Start()
    {
        healthBarUIScript = GameObject.Find("UIHealthbar").GetComponent<PlayerHealthbarScript>();
        _currentHealth = fullHealth;
        FindBoundaries();
    }

    /// <summary>
    /// Calculates the boundaries of the screen so planes can't go off screen.
    /// </summary>
    private void FindBoundaries()
    {
        var mainCamera = Camera.main;
        if (mainCamera != null)
        {
            _minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            _maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            _minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            _maxY = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - (padding * 2.5f);
        }
    }
    
    // When bullet hits enemy, enemy takes damage and health bar is updated.
    private void DamageHealthBar(float damage)
    {
        if (!(_currentHealth > 0)) return;
        _currentHealth -= damage;
        healthBar.SetSize(_currentHealth/ fullHealth);
        healthBarUIScript.SetHealth(_currentHealth / fullHealth);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            DamageHealthBar(col.gameObject.GetComponent<Bullet>().damage);
            Destroy(col.gameObject);
            if (_currentHealth <= 0)
            {
                var explosion = Instantiate(particleBlast, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(col.gameObject);
                Destroy(explosion, 2f);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        
        Vector2 planePosition = transform.position;
        var newXPosition = Mathf.Clamp(planePosition.x + horizontalInput, _minX, _maxX);
        var newYPosition = Mathf.Clamp(planePosition.y + verticalInput, _minY, _maxY);
        
        transform.position = new Vector2(newXPosition, newYPosition);
    }
}
