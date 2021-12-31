using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject levelSelection;

    public string gameSceneName;

    [HideInInspector]
    public int egg1;
    [HideInInspector] public int egg2;
    [HideInInspector] public int egg3;
    [HideInInspector] public int totEgg1;
    [HideInInspector] public int totEgg2;
    [HideInInspector] public int totEgg3;
    [HideInInspector] public bool isLevel2;
    [HideInInspector] public bool isLevel3;
    public AudioMixer mix;

    public Slider musicSlider;
    public Slider sfxSlider;


    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        levelSelection.SetActive(false);
        totEgg1 = PlayerPrefs.GetInt("totEgg1", -1);
        totEgg2 = PlayerPrefs.GetInt("totEgg2", -1);
        totEgg3 = PlayerPrefs.GetInt("totEgg3", -1);
        egg1 = PlayerPrefs.GetInt("egg1", -1);
        egg2 = PlayerPrefs.GetInt("egg2", -1);
        egg3 = PlayerPrefs.GetInt("egg3", -1);
        isLevel2 = PlayerPrefs.GetInt("2",0) == 1 ;
        isLevel3 = PlayerPrefs.GetInt("3",0) == 1;
        musicSlider.value = PlayerPrefs.GetInt("musicVol", 0);
        sfxSlider.value= PlayerPrefs.GetInt("sfxVol", 0);

        Debug.Log(totEgg1);
        Debug.Log(egg1);

    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ApplySettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        levelSelection.SetActive(false);
    }

    public void OpenLevelSelection()
    {
        int[] eggs = { egg1, egg2, egg3 };
        int[] tot = { totEgg1, totEgg2, totEgg3 };
        Transform body = levelSelection.transform.GetChild(1).transform;
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
        if (isLevel3)
        {
            body.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.8666667f, 0.4509804f, 0.1607843f, 1);
        }
        else
        {
            body.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.6698113f, 0.5004979f, 0.382298f, 0.5372549f);
        }
        if (isLevel2)
        {
            body.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.8666667f, 0.4509804f, 0.1607843f, 1);
        }
        else
        {
            body.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.6698113f, 0.5004979f, 0.382298f, 0.5372549f);
        }
        
        for (int i =0; i<3; i++)
        {
            GameObject oeuf = body.GetChild(i).transform.GetChild(2).gameObject;
            if (eggs[i]>0)
            {
                Debug.Log(i);
                Debug.Log(eggs[i]);
                
                oeuf.SetActive(true);
                oeuf.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = eggs[i].ToString();
                oeuf.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = tot[i].ToString();

            }
            else
            {
                oeuf.SetActive(false);
            }
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene("Niveau 1");
    }

    public void Level2()
    {
        if (isLevel2)
        { SceneManager.LoadScene("Niveau 2"); }
    }

    public void Level3()
    {
        if (isLevel3)
        { SceneManager.LoadScene("Niveau 3"); }
    }

    public void SetMusicVolume(float vol)
    {
        mix.SetFloat("MusicVol", vol);
        PlayerPrefs.SetFloat("musicVol", vol);
    }

    public void SetSFXVolume(float vol)
    {
        if (vol==-30)
        {
            vol = -80;
        }
        mix.SetFloat("SFXVol", vol);
        PlayerPrefs.SetFloat("sfxVol", vol);
    }

    public void SetUIVolume(float vol)
    {
        mix.SetFloat("UIVol", vol);
    }

}
