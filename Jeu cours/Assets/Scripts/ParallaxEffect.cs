using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPos;
    public float parallaxEffect;
    public GameObject cam;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector2(startPos + dist, transform.position.y);

        if (temp > startPos + length) { startPos += length; }
        else if (temp < startPos - length) {startPos -= length; }
    }
}
