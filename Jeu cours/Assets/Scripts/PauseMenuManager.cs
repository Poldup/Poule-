using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public string mainMenuSceneName;
    public string nextSceneName;
    public AudioMixer mix;

    public Slider musicSlider;
    public Slider sfxSlider;

    private bool paused = false;

    void Start()
    {
        ResumeGame();
        mix.GetFloat("MusicVol", out float musicVolVal);
        musicSlider.value = musicVolVal;
        mix.GetFloat("SFXVol", out float SFXVolVal);
        sfxSlider.value = SFXVolVal;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused) ResumeGame();
            else PauseGame();
        }
    }
    

    public void PauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToCheckpoint()
    {
        GameManager.Instance.ReturnToCheckpoint();
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ApplySettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void SetMusicVolume(float vol)
    {
        mix.SetFloat("MusicVol", vol);
        PlayerPrefs.SetFloat("musicVol", vol);
    }

    public void SetSFXVolume(float vol)
    {
        if (vol == -30)
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
