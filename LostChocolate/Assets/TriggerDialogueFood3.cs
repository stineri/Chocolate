using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogueFood3 : MonoBehaviour
{
    public DialogueManager dialogueManager;


    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartFood3Dialogue();
        }
    }

    void StartFood3Dialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Pumpkin - Safe Food", "Good for digestion, nutritious but not exciting for dogs.")
        };

        dialogueManager.StartDialogue(lines);
    }
}
