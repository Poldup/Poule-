using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathzone : MonoBehaviour
{
    public float knockbackForce;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.GetComponent<PlayerControler>() != null)
        {
            StartCoroutine(PlayerControler.Instance.Knockback(transform.position.x, knockbackForce));
            GameManager.Instance.TakeDamage();
        }
    }
}

