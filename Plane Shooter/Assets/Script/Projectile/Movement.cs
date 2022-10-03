using UnityEngine;

public class Movement : MonoBehaviour
{
    private void Start()
    {
        
    }

    void Update()
    {
        transform.position += 10f * Time.deltaTime * Vector3.up; 
    }
}
