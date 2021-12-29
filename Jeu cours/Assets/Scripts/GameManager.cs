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
    public int heart;
    private bool loosingLife;

    public List<GameObject> pickedEggs = new List<GameObject>();
    public List<GameObject> pickedGFeathers = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        camFoActive = true;
    }

    private void Start()
    {
        lives = 3;
        heart = 3;
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
        if (lives < 0 && !loosingLife)
        {
            StartCoroutine(LifeLost());
        }
        
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
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public IEnumerator LifeLost()
    {
        loosingLife = true;
        camFoActive = false;
        CircleCollider2D[] circleCollider2D;
        circleCollider2D = player.GetComponents<CircleCollider2D>();
        PlayerControler.Instance.canMove = false;
        
        circleCollider2D[0].enabled = false;
        circleCollider2D[1].enabled = false;
        player.GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        if (heart < 0)
        {
            GameOver();
        }
        else
        {
            lives = 3;
            heart -= 1;
            circleCollider2D[0].enabled = true;
            circleCollider2D[1].enabled = true;
            player.GetComponent<PolygonCollider2D>().enabled = true;
            ReturnToCheckpoint();
            PlayerControler.Instance.canMove = true;
            camFoActive = true;
            loosingLife = false;
        }
    }

    public void Pickoeuf(GameObject oeuf)
    {
        pickedEggs.Add(oeuf);
        eggs += 1;
    }

    public void PickGPlume(GameObject plume)
    {
        pickedGFeathers.Add(plume);
        goldenPlumes += 1;
    }

    public void ReturnToCheckpoint()
    {
        player.transform.position = playerSpawn.transform.position + new Vector3(0, 2, 0);
        offset = cam.GetComponent<CameraFollow>().offset;
        cam.transform.position = playerSpawn.transform.position + offset;
        eggs -= pickedEggs.Count;
        foreach (GameObject oeuf in pickedEggs)
        {
            oeuf.SetActive(true);
        }
        pickedEggs.Clear();
        goldenPlumes -= pickedGFeathers.Count;
        foreach (GameObject plume in pickedGFeathers)
        {
            plume.SetActive(true);
        }
        pickedGFeathers.Clear();
    }

}