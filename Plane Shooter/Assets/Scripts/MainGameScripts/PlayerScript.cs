using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        var playerPos = transform.position;
        
        var newXPos = playerPos.x + deltaX;
        var newYPos = playerPos.y + deltaY;
        
        transform.position = new Vector2(newXPos, newYPos);
    }
}
