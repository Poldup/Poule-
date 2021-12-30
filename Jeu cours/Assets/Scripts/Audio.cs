using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource flap1;
    public AudioSource flap2;
    public AudioSource bigflap;

    void Flap1()
    {
        flap1.Play();
    }

    void Flap2()
    {
        flap2.Play();
    }

    void BigFlap()
    {
        bigflap.Play();
    }
}

