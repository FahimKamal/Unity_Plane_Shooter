using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float shootRepeatTime = 0.3f;
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject muzzleFlash2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        Instantiate(bulletPrefab, spawnPoint1.position, Quaternion.identity);
        Instantiate(bulletPrefab, spawnPoint2.position, Quaternion.identity);
        muzzleFlash.SetActive(true);
        muzzleFlash2.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        muzzleFlash2.SetActive(false);
        yield return new WaitForSeconds(shootRepeatTime);
        StartCoroutine(Shoot());
    }
}
