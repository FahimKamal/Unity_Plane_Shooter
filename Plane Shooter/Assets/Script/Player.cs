using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }

        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 10, Input.GetAxis("Vertical") * Time.deltaTime * 10, 0.0f);
        
    }
}
