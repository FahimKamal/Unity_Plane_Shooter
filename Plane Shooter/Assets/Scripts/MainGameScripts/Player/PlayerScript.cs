using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float fullHealth = 100f;
    private float _currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerHealthbarScript healthBarUIScript;
    [SerializeField] private GameObject damageEffect;
    [SerializeField] private GameObject particleBlast;
    [SerializeField] private GameController gameController;
    
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float padding = 0.8f;

    [SerializeField] public AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] public AudioClip shootSound;
    [SerializeField] private AudioClip coinCollectSound;
    
    private int _coins = 0;
    [SerializeField] CoinCount coinCountUIScript;
    
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
        // If Player collides with bullet, take damage.
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            DamageHealthBar(col.gameObject.GetComponent<Bullet>().damage);
            Destroy(col.gameObject);
            var damageVfx = Instantiate(damageEffect, col.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.05f);
            audioSource.PlayOneShot(hitSound, 0.5f);
            
            // If player health is 0, destroy player.
            if (_currentHealth <= 0)
            {
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, 0.5f);
                var explosion = Instantiate(particleBlast, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(col.gameObject);
                Destroy(explosion, 2f);
                gameController.GameOver();
            }
        }

        // Collect coins
        if (col.gameObject.CompareTag("Coin"))
        {
            Destroy(col.gameObject);
            _coins++;
            coinCountUIScript.AddCoin(_coins);
            audioSource.PlayOneShot(coinCollectSound, 0.5f);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        #region Input for PC
        // BUT NOW THEY WILL BE COMMENT OUTED.
        var horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var verticalInput = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        
        var planePosition = transform.position;
        var newXPosition = Mathf.Clamp(planePosition.x + horizontalInput, _minX, _maxX);
        var newYPosition = Mathf.Clamp(planePosition.y + verticalInput, _minY, _maxY);
        
        transform.position = new Vector2(newXPosition, newYPosition);

        #endregion
        
        #region INPUT FOR MOBILE

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            planePosition = transform.position;
            newXPosition = Mathf.Clamp(mousePosition.x, _minX, _maxX);
            newYPosition = Mathf.Clamp(mousePosition.y, _minY, _maxY);
            transform.position = Vector2.Lerp(planePosition, new Vector2(newXPosition, newYPosition), movementSpeed * Time.deltaTime);
        }

        #endregion
        
    }
}
