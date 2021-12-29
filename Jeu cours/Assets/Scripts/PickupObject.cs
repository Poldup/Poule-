using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private bool canDo=true;

    


    private void OnTriggerEnter2D (Collider2D collision)
    {

      
        if (collision.CompareTag("Poule"))
        {
            if (canDo)
            {
                canDo = false;
                StopCoroutine(Disappear());
                StartCoroutine(Disappear());
                GameManager.Instance.AddPlume(1);
            }
        }
        
    }
    IEnumerator Disappear()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(3);
        while (PlayerControler.Instance.isGrounded == false)
        {
            
            yield return new WaitForSeconds(.1f);
        }
        canDo = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield break;
    }
}
