using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogueFood5 : MonoBehaviour
{
    public DialogueManager2 dialogueManager2;


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
            new DialogueLine("Fish - Safe Food", "Contains good protein and omega-3 source, healthy but slightly less appealing than chicken")
        };

        dialogueManager2.StartDialogue(lines);
    }
}
