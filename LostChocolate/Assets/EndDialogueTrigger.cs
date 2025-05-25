using UnityEngine;
using System.Collections.Generic;

public class EndDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioClip finalClip; // Optional voice clip

    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;

            List<DialogueLine> lines = new List<DialogueLine>()
            {
                new DialogueLine("Choco", "Buti nakauwi na ako…", finalClip)
            };

            dialogueManager.StartDialogue(lines);
        }
    }
}
