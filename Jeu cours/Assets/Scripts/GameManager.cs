using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource musique;
    public int fraises;
    public int goldenPlumes;
    public int plumes = 0;
    private bool musicPlayed;
    public bool musicOn;

    private void Awake()
    {
        Instance = this;
        
    }

    void Update()
    {
        if (fraises>0 && musicPlayed==false && musicOn)
        {
            musicPlayed = true;
            if (musicPlayed)
            {
                musique.Play();
            }
        }
        
        if (musicPlayed && !musicOn)
        {
            musique.Stop();
            musicPlayed = false;
        }
    }

    public void AddFraise(int nb)
    {
        fraises += nb;
    }

    public void AddPlume(int nb)
    {
        plumes += nb;
        PlayerControler.Instance.comptPlumes += nb;
    }

    public void ResetPlumes()
    {
        plumes = 0;
    }



}
