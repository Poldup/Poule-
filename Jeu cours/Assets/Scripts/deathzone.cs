using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathzone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.GetComponent<PlayerControler>() != null)
        {
            other.transform.GetComponent<PlayerControler>().GameOver();
        }
    }
}

