using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathzone : MonoBehaviour
{
    public float knockbackForce;
    public float knockbackTimer;
    public bool destroyOnCollision;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.GetComponent<PlayerControler>() != null)
        {
            Vector2 contact = other.GetContact(0).point;

            if(!other.transform.GetComponent<PlayerControler>().isInvincible)
            {
                StartCoroutine(PlayerControler.Instance.Knockback(contact,true));
                //PlayerControler.Instance.Knockback(knockbackTimer, knockbackForce, contact);
                //GameManager.Instance.TakeDamage();
            }

            if(destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.GetComponent<PlayerControler>() != null)
        {
            
            if (!collider.transform.GetComponent<PlayerControler>().isInvincible)
            {
                StartCoroutine(PlayerControler.Instance.Knockback(transform.position, false));
                //PlayerControler.Instance.Knockback(knockbackTimer, knockbackForce, transform.position);
                //GameManager.Instance.TakeDamage();
                
            }

            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}

