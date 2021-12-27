using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReach : MonoBehaviour
{
    public GameObject playerSpawn;
    private Transform pS;
    private void Awake()
    {
        pS = playerSpawn.transform;

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            pS.position = transform.position;
        }
    }
}
