using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogueFood1 : MonoBehaviour
{
    public DialogueManager dialogueManager;
    

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartFood1Dialogue();
        }
    }

    void StartFood1Dialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Safe Food", "Contains good protein and omega-3 source, healthy but slightly less appealing than chicken")
        };

        dialogueManager.StartDialogue(lines);
    }
}
