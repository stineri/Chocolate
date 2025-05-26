using UnityEngine;
using System.Collections.Generic;

public class triggerDialogueFood10 : MonoBehaviour
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
            new DialogueLine("Cheese - Not Safe", "Often high in fat and lactose, which may upset a dog’s stomach.")
        };

        dialogueManager.StartDialogue(lines);
    }
}
