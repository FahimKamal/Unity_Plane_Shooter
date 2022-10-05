using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum moveDirection
{
    Up,
    Down,
};

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private moveDirection direction;

    private Vector2 _directionVector;
    private float _yMin;
    private float _yMax;

    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main != null)
        {
            _yMax = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;
            _yMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
            
        }

        _directionVector = direction == moveDirection.Up ? Vector2.up : Vector2.down;
    }
    
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(_directionVector * (Time.deltaTime * speed));

        DestroyObject();
    }
    
    private void DestroyObject()
    {
        if (direction == moveDirection.Up && transform.position.y > _yMax)
        {
            Destroy(gameObject);
        }
        else if (direction == moveDirection.Down && transform.position.y < _yMin)
        {
            Destroy(gameObject);
        }
    }
}
