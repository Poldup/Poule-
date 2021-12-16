using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool cantDo;


    private void OnTriggerEnter2D (Collider2D collision)
    {
        
        if (collision.CompareTag("Poule") && !cantDo)
        {
            cantDo = true;
            Destroy(gameObject);
            GameManager.Instance.AddPlume(1);
        }
        
    }
}
