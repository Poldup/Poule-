using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject endMenu;
    private bool ended;

    private void Awake()
    {
        endMenu.SetActive(false);
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
    }

    IEnumerator EndMenu()
    {
        endMenu.SetActive(true);
        yield return new WaitForSeconds(.7f);
        int eggs = GameManager.Instance.eggs;
        for (int egg = 1; egg <= eggs; egg++)
        {
            endMenu.transform.GetChild(1).transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = egg.ToString();
            GameManager.Instance.eggs -= 1;
            yield return new WaitForSeconds(.1f);
        }

    }
}

