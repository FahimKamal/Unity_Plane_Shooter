using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float shootRepeatTime = 0.3f;
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] muzzleFlashs;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        // Spawn bullet
        foreach (var spawnPoint in spawnPoints) Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        
        // Spawn muzzle flash
        foreach (var muzzleFlash in muzzleFlashs) muzzleFlash.SetActive(true);
        
        
        // Wait for a bit and then disable muzzle flash
        yield return new WaitForSeconds(0.09f);
        foreach (var muzzleFlash in muzzleFlashs) muzzleFlash.SetActive(false);
        
        // Wait for a bit and then repeat
        yield return new WaitForSeconds(shootRepeatTime);
        StartCoroutine(Shoot());
    }
}
