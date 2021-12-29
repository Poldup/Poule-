using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueDeathzone : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other )
    {
        if (other.CompareTag("Poule")&& !GameManager.Instance.loosingLife)
        {
            StartCoroutine(GameManager.Instance.LifeLost());
        }
    }

  
}
