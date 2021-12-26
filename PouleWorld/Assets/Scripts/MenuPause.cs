using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject panelMenuPause;

    private bool paused = false;

    void Start()
    {
        ResumeGame();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if(paused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;
        panelMenuPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        panelMenuPause.SetActive(false);
        Time.timeScale = 1;
    }
}
