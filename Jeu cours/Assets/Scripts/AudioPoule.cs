using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoule : MonoBehaviour
{
    public AudioSource pouleAudio;
    public AudioSource hurtAudio;
    public AudioClip flap1;
    public AudioClip flap2;
    public AudioClip bigflap;
    public List<AudioClip> hurt;
    public List<AudioClip> walk;

    void Flap1()
    {
        pouleAudio.clip = flap1;
        pouleAudio.volume = 0.2f;
        pouleAudio.Play();
    }

    void Flap2()
    {
        pouleAudio.clip = flap2;
        pouleAudio.volume = 0.2f;
        pouleAudio.Play();
    }

    void BigFlap()
    {
        pouleAudio.clip = bigflap;
        pouleAudio.volume = 0.3f;
        pouleAudio.Play();
    }

    void Walk()
    {
        int num = Random.Range(0, 4);
        pouleAudio.clip = walk[num];
        pouleAudio.volume = 0.05f;
        pouleAudio.Play();
    }

    public void Hurt()
    {
        int num = Random.Range(0, 2);
        Debug.Log(num);
        hurtAudio.clip = hurt[num];
        hurtAudio.volume = .3f;
        hurtAudio.Play();
    }
}

