using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    


    private void OnTriggerEnter2D (Collider2D collision)
    {

        Debug.Log("collision");
        if (collision.CompareTag("Poule"))
        {
            
            Debug.Log("Contact");
            StopCoroutine(Disappear());
            StartCoroutine(Disappear());
            GameManager.Instance.AddPlume(1);
        }
        
    }
    IEnumerator Disappear()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        Debug.Log("début du countdown");
        yield return new WaitForSeconds(3);
        Debug.Log("fin du countdown");
        while (PlayerControler.Instance.isGrounded == false)
        {
            Debug.Log("n'est pas au sol");
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log("devrait réapparaître");
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield break;
    }
}
