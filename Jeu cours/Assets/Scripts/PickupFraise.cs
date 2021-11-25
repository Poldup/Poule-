using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFraise : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            Destroy(gameObject);
            GameManager.Instance.AddFraise(1);
            
        }
    }


}
