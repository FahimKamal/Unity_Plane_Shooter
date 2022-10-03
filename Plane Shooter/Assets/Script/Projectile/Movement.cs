using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Update()
    {
        transform.position += 10f * Time.deltaTime * Vector3.up; 
    }
}
