using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static bool isDialogueActive = false;
    public GameObject dialogueCanvas;
    public Text speakerText;
    public Text dialogueText;
    public Text continueText;
    public float continueFlashSpeed = 0.7f;

    void Update()
    {
        if (isDialogueActive && !PauseMenu.isGamePaused)
        {
            continueText.color = new Color(255, 255, 255, Mathf.PingPong(Time.unscaledTime * continueFlashSpeed, 1f));
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ResumeGame();
            }
        }
    }

    public void TriggerDialogue(string speaker, string text)
    {
        isDialogueActive = true;
        dialogueText.text = text;
        speakerText.text = speaker;
        dialogueCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        isDialogueActive = false;
        dialogueCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
