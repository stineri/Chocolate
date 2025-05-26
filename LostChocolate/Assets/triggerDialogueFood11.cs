using UnityEngine;
using System.Collections.Generic;

public class triggerDialogueFood11 : MonoBehaviour
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
            new DialogueLine("Chocolate - Not Safe", "Contains theobromine, which dogs can’t process and can harm their heart and brain.")
        };

        dialogueManager.StartDialogue(lines);
    }
}
