using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 offset;

    private Vector3 velocity;
    public bool activated;

    private void Awake()
    {
        activated = true;
    }
    void Update()
    {
        activated = GameManager.Instance.camFoActive;
        if (activated)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, timeOffset);
        }
        
    }
}
