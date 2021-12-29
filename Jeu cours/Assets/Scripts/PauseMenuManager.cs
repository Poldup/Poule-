using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public string mainMenuSceneName;
    public string nextSceneName;

    private bool paused = false;

    void Start()
    {
        ResumeGame();
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
}
