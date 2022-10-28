using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    public void PlayGame()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Destroy(GameObject.Find("Audio Manager"));
        Destroy(GameObject.Find("LevelManager"));
        Destroy(GameObject.Find("Player"));

        SceneManager.LoadScene("Start Menu");
        Destroy(GameObject.Find("HUD"));
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }
}
