using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject cam;
    private Vector3 offset;
    public bool camFoActive;
    public GameObject player;
    public GameObject playerSpawn;
    public AudioSource musique;
    public int eggs;
    public int goldenPlumes;
    public int plumes = 0;
    private bool musicPlayed;
    public bool musicOn;
    public int lives;


    private void Awake()
    {
        Instance = this;
        camFoActive = true;
    }

    private void Start()
    {
        lives = 3;
    }

    void Update()
    {
        if (eggs > 0 && musicPlayed == false && musicOn)
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

        if (lives < 0)
        {
            GameOver();
        }
    }

    public void AddEggs(int nb)
    {
        eggs += nb;
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

    public void TakeDamage()
    {
        lives -= 1;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");

        string sceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public IEnumerator LifeLost()
    {
        
        camFoActive = false;
        
        yield return new WaitForSeconds(2);
        lives = 3;
        player.transform.position = playerSpawn.transform.position + new Vector3 (0,2,0);
        offset = cam.GetComponent<CameraFollow>().offset;
        cam.transform.position = playerSpawn.transform.position + offset;
        camFoActive = true;

    }

}