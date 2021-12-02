using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathzone : MonoBehaviour
{
    public float knockbackForce;
    public float knockbackTimer;
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.transform.GetComponent<PlayerControler>() != null)
        {
            Vector2 contact = other.GetContact(0).point;
            //StartCoroutine(PlayerControler.Instance.Knockback(transform.position.x, knockbackForce));
            PlayerControler.Instance.Knockback(knockbackTimer,knockbackForce, contact);
            GameManager.Instance.TakeDamage();
        }
    }
}

