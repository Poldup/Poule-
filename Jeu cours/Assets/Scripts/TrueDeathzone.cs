using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueDeathzone : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other )
    {
        if (other.CompareTag("Poule"))
        {
            StartCoroutine(GameManager.Instance.LifeLost());
        }
    }

  
}
