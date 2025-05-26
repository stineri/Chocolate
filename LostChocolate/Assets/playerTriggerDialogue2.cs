using UnityEngine;
using System.Collections.Generic;

public class playerTriggerDialogue2 : MonoBehaviour
{
    public DialogueManager2 dialogueManager2;

    // Optional: ensure it only triggers once
    private bool hasTriggered = false;

    // Audio clip example
    public AudioClip playerVoiceClip1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartPlayerDialogue();
        }
    }

    void StartPlayerDialogue()
    {
        List<DialogueLine> playerLines = new List<DialogueLine>()
        {
            new DialogueLine("Choco", "Hala! Kailangan ko siyang matakasan!", playerVoiceClip1),
            new DialogueLine("Tip", "Gamitin ang W, A, S, D, Shift para umiwas at tumakbo nang mabilis!")

        };

        dialogueManager2.StartDialogue(playerLines);
    }
}
