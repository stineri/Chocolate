using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public AudioSource audioSource; // Add this in Unity Inspector

    private Queue<DialogueLine> dialogueLines = new Queue<DialogueLine>();
    private bool isDialogueActive = false;

    void Update()
    {
        if (isDialogueActive && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)))
        {
            ShowNextLine();
        }
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        dialogueLines.Clear();

        foreach (DialogueLine line in lines)
        {
            dialogueLines.Enqueue(line);
        }

        dialoguePanel.SetActive(true);
        isDialogueActive = true;

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = dialogueLines.Dequeue();
        nameText.text = line.speakerName;
        dialogueText.text = line.text;

        // Play voice if available
        if (line.voiceClip != null)
        {
            audioSource.Stop(); // Optional: stop current audio before playing next
            audioSource.clip = line.voiceClip;
            audioSource.Play();
        }
        else
        {
            audioSource.Stop(); // Stop audio if no voice for this line
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
        audioSource.Stop();
    }
}
