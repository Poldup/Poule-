using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource musique;
    public int fraises;
    public int goldenPlumes;
    public int plumes =0;
    private bool musicPlayed;
    public bool musicOn;
    public GameObject menu;
    private bool menuOn;
    


    private void Awake()
    {
        Instance = this;
        menu.SetActive(false);
        menuOn = false;
        
    }

    void Update()
    {

        if (Input.GetKeyDown("p"))
        { ActivateMenu(); }
        
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

    public void ActivateMenu()
    {
        if (!menuOn)
        {
            menu.SetActive(true);
            menuOn = true;
            Time.timeScale = 0;

        }

        else if (menuOn)
        {
            menu.SetActive(false);
            menuOn = false;
            Time.timeScale = 1;

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
    public void AddGPlume(int nb)
    {
        goldenPlumes += nb;
    }

    public void ResetPlumes()
    {
        plumes = 0;
    }

    public void Restart()
    {
        Debug.Log("Restart");

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    }
