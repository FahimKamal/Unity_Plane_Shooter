using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    private float _yMax;
    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main != null) _yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);
        
        if(transform.position.y > _yMax)
        {
            Destroy(gameObject);
        }
    }
}
