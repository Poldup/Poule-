using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxEffectX;
    public float parallaxEffectY;
    public GameObject cam;

    [Header("Offset Position")]
    public float offsetX;
    public float offsetY;

    private float length;

    void Start()
    {
        transform.position = new Vector2(offsetX, offsetY)  ;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }


    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectX));
        float distX = (cam.transform.position.x * parallaxEffectX);
        float distY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector2(offsetX + distX, cam.transform.position.y + offsetY - distY);

        if (temp > offsetX + length) { offsetX += length; }
        else if (temp < offsetY - length) {offsetY -= length; }
    }
}
