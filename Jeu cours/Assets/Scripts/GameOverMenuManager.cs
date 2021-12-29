using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public string gameSceneName;
    public string mainMenuSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
