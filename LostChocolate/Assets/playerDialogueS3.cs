using UnityEngine;
using System.Collections.Generic;

public class playerDialogueS3 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Optional: ensure it only triggers once
    private bool hasTriggered = false;

    // Audio clip example
  
    public AudioClip line5Clip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartEnemyDialogue2();
        }
    }

    void StartEnemyDialogue2()
    {
        List<DialogueLine> playerLines = new List<DialogueLine>()
        {
            
            new DialogueLine("Choco", "Ay, ako ‘yon ah! Kailangan ko nang makatakas at makauwi!\nAy, ako ‘yon ah! Kailangan ko nang makatakas at makauwi!", line5Clip)

        };

        dialogueManager.StartDialogue(playerLines);
    }
}
