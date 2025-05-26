using UnityEngine;
using System.Collections.Generic;

public class triggerDialogueFood9 : MonoBehaviour
{
    public DialogueManager dialogueManager;


    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartFood4Dialogue();
        }
    }

    void StartFood4Dialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Banana - Safe Food", "Occasional treat, sweet, but too much isn’t great for dogs.")
        };

        dialogueManager.StartDialogue(lines);
    }
}
