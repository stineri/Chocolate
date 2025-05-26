using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogueFood2 : MonoBehaviour
{
    public DialogueManager dialogueManager;


    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartFood2Dialogue();
        }
    }

    void StartFood2Dialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Garlic - Not Safe", "Can damage red blood cells, potentially causing anemia.")
        };

        dialogueManager.StartDialogue(lines);
    }
}
