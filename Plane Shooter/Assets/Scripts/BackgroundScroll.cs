using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Renderer MeshRenderer;
    public float ScrollSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // var offset = MeshRenderer.material.mainTextureOffset;
        // offset = offset + new Vector2(0.00f, ScrollSpeed * Time.deltaTime);
        MeshRenderer.material.mainTextureOffset += new Vector2(0.00f, ScrollSpeed * Time.deltaTime);
    }
}
