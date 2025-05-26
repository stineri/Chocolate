using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogueFood4 : MonoBehaviour
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
            new DialogueLine("Chicken - Safe Food", "Highly nutritious, protein-rich, a favorite for dogs.")
        };

        dialogueManager.StartDialogue(lines);
    }
}
