using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;         // Assign in Inspector
    public GameObject dialoguePanel;  // Assign in Inspector

    // Call this to start showing dialogue
    public void StartDialogue(string dialogueLine)
    {
        dialoguePanel.SetActive(true);         // Show dialogue panel
        dialogueText.text = dialogueLine;      // Set dialogue text
    }

    // Optional: Hide dialogue after a few seconds
    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
