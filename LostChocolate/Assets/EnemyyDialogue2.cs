using UnityEngine;
using System.Collections.Generic;

public class EnemyyDialogue2 : MonoBehaviour
{
    public DialogueManager2 dialogueManager2;

    // Optional: ensure it only triggers once
    private bool hasTriggered = false;

    // Audio clip example
    public AudioClip enemyVoiceClip2;
    public AudioClip enemyVoiceClip3;

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
            new DialogueLine("Dog Catcher", "Hoy! Bumaba ka riyan, aso!", enemyVoiceClip2),
            new DialogueLine("Dog Catcher", "Ay naku, wala na…", enemyVoiceClip3)

        };

        dialogueManager2.StartDialogue(playerLines);
    }
}
