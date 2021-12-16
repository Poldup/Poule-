using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFraise : MonoBehaviour
{
    private bool cantDo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule") && !cantDo)
        {
            cantDo = true;
            Destroy(gameObject);
            GameManager.Instance.AddFraise(1);
            
        }
    }


}
