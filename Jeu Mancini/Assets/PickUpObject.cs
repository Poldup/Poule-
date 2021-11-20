using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        PlayerControler.Instance.AddPlume();
    }
}
