using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public static bool isGameOver = false;

    public void TriggerGameOver()
    {
        gameOverCanvas.SetActive(true);
        isGameOver = true;
    }

    public void ReturnToMainMenu()
    {
        isGameOver = false;
        SceneManager.LoadScene("MainMenu");
    }
}
