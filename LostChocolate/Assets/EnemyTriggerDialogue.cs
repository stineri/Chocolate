using UnityEngine;
using System.Collections.Generic;

public class EnemyTriggerDialogue : MonoBehaviour
{
    public DialogueManager2 dialogueManager2;

    // Optional: ensure it only triggers once
    private bool hasTriggered = false;

    // Audio clip example
    public AudioClip enemyVoiceClip1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Enemy"))
        {
            hasTriggered = true;
            StartEnemyDialogue();
        }
    }

    void StartEnemyDialogue()
    {
        List<DialogueLine> enemyLines = new List<DialogueLine>()
        {
            new DialogueLine("Animal Catcher", "Oy! Hulihin ‘yang askal na ‘yan bago pa makagat ‘yung mga tao!", enemyVoiceClip1)
            
        };

        dialogueManager2.StartDialogue(enemyLines);
    }
}
