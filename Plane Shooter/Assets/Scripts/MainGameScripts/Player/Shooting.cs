using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    private void Shoot()
    {
        Instantiate(bulletPrefab, spawnPoint1.position, Quaternion.identity);
        Instantiate(bulletPrefab, spawnPoint2.position, Quaternion.identity);
    }
}
