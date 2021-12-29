using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public bool devMode;
    public GameObject player;
    public GameObject cam;
    private Vector3 offset;
    


    void Start()
    {
        if (!devMode)
        {
            offset = cam.GetComponent<CameraFollow>().offset;
            player.transform.position = transform.position;
            cam.transform.position = transform.position + offset;
        }
    }
}
