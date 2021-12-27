using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length;
    private Vector3 startPos;

    public float parallaxEffectX;
    public float parallaxEffectY;
    public GameObject cam;

    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectX));
        float distX = (cam.transform.position.x * parallaxEffectX);
        float distY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector2(startPos.x + distX, startPos.y + distY);

        if (temp > startPos.x + length) { startPos.x += length; }
        else if (temp < startPos.x - length) {startPos.x -= length; }
    }
}
