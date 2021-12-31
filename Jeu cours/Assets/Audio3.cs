using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Audio3 : MonoBehaviour
{
    public GameObject player;
    public AudioClip deb;
    public AudioClip fin;
    public AudioSource source;
    private AudioClip next;

    private void Awake()
    {
        source.clip = deb;
        source.Play();
    }

    private void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            next = fin;
        }
        else
        {
            next = deb;
        }
        if(!source.isPlaying)
        {
            source.clip = next;
            source.Play();
        }
    }
}
