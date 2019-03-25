using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{

    public float speedX = 0.5f;
    public float speedY = 0.5f;
    private Material mat;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (mat != null)
        {
            var offsetX = Time.time * speedX;
            var offsetY = Time.time * speedY;
            mat.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }
}
