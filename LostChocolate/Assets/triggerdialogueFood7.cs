using UnityEngine;
using System.Collections.Generic;


public class triggerdialogueFood7 : MonoBehaviour
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
            new DialogueLine("Chicken - Safe Food", "Highly nutritious, protein-rich, a favorite for dogs.")
        };

        dialogueManager2.StartDialogue(lines);
    }
}
