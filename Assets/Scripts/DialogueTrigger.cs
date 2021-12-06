using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool isSingleUse = false;
    private bool used = false;
    public DialogueManager dialogueManager;
    public string speaker;
    [TextArea]
    public string dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSingleUse || (isSingleUse && !used))
        {
            if (collision.gameObject.tag == "Player")
            {
                used = true;
                dialogueManager.TriggerDialogue(speaker, dialogue);
                if(isSingleUse){
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
