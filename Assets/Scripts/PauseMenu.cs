using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                if (!GameOver.isGameOver)
                {
                    PauseGame();
                }
            }
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        pauseMenuCanvas.SetActive(false);
        if (!DialogueManager.isDialogueActive)
        {
            Time.timeScale = 1f;
        }
    }

    public void ReturnToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {   
        Application.Quit();
    }
}
