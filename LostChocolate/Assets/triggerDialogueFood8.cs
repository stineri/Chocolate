using UnityEngine;
using System.Collections.Generic;

public class triggerDialogueFood8 : MonoBehaviour
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
            new DialogueLine("Cheese - Not Safe", "Often high in fat and lactose, which may upset a dog’s stomach.")
        };

        dialogueManager2.StartDialogue(lines);
    }
}
