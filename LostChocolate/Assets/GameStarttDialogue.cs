using UnityEngine;
using System.Collections;

public class GameStartDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        StartCoroutine(ShowDialogueSequence());
    }

    IEnumerator ShowDialogueSequence()
    {
        dialogueManager.StartDialogue("Ay, ang dami palang gala dito sa kalsada...");
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        dialogueManager.StartDialogue("Kailangan ko talagang mag-ingat");
        yield return new WaitForSeconds(3f); // Show second line for 3 seconds

        dialogueManager.EndDialogue(); // Hide the panel (optional)
    }
}
