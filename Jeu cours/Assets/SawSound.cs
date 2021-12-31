using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSound : MonoBehaviour
{
    public AudioClip coupe;
    public AudioClip idle;
    public AudioSource source;

    private void Awake()
    {
        source.clip = idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {
            source.pitch = 1.8f;
            source.clip = coupe;
            source.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Poule"))
        {

            source.pitch = 1;
            source.clip = idle;
            source.Play();
        }
    }

    
}
