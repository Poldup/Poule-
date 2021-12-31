using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject endMenu;
    private bool ended;
    public GameObject egglist;
    private int eggTotal;
    public AudioSource son;
    public int lvlnumber;

    private void Awake()
    {
        endMenu.SetActive(false);
        eggTotal = egglist.transform.childCount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerControler>() != null&& !ended)
        {
            StartCoroutine(LevelEnd());
            ended = true;
        }
    }

    IEnumerator LevelEnd()
    {
        GameManager.Instance.camFoActive = false;
        PlayerControler.Instance.playing = false;
        PlayerControler.Instance.axeHorizontal = 1;
        yield return new WaitForSeconds(5);
        PlayerControler.Instance.axeHorizontal = 0;
        StartCoroutine(EndMenu());
        if (lvlnumber==1)
        {
            int current =PlayerPrefs.GetInt("egg1", -1);
            if (GameManager.Instance.eggs > current)
            { PlayerPrefs.SetInt("egg1", GameManager.Instance.eggs); }
            Debug.Log(GameManager.Instance.eggs);
            PlayerPrefs.SetInt("totEgg1", eggTotal);
            PlayerPrefs.SetInt("2", 1);
        }
        if (lvlnumber == 2)
        {
            int current = PlayerPrefs.GetInt("egg2", -1);
            if (GameManager.Instance.eggs > current)
            { PlayerPrefs.SetInt("egg2", GameManager.Instance.eggs); }
            Debug.Log(GameManager.Instance.eggs);
            PlayerPrefs.SetInt("totEgg2", eggTotal);
            PlayerPrefs.SetInt("3", 1);
        }
        if (lvlnumber == 3)
        {
            int current = PlayerPrefs.GetInt("egg3", -1);
            if (GameManager.Instance.eggs > current)
            { PlayerPrefs.SetInt("egg3", GameManager.Instance.eggs); }
            Debug.Log(GameManager.Instance.eggs);
            PlayerPrefs.SetInt("totEgg3", eggTotal);
        }



    }

    IEnumerator EndMenu()
    {
        endMenu.SetActive(true);
        yield return new WaitForSeconds(.7f);
        int eggs = GameManager.Instance.eggs;
        Transform body = endMenu.transform.GetChild(1).transform;
        body.GetChild(3).gameObject.SetActive(true);
        for (int egg = 1; egg <= eggs; egg++)
        {
            body.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = egg.ToString();
            GameManager.Instance.eggs -= 1;
            son.Play(); 
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.4f);
        body.GetChild(4).gameObject.SetActive(true);

        body.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = eggTotal.ToString();
        body.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        if (eggs==eggTotal)
        {
            body.GetChild(6).gameObject.SetActive(true);
        }
        else
        {
            body.GetChild(7).gameObject.SetActive(true);
        }


    }
}

