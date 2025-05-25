using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject choicesPanel;
    public Button[] choiceButtons;

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
        foreach (var line in lines)
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

        if (line.voiceClip != null)
        {
            GetComponent<AudioSource>().PlayOneShot(line.voiceClip);
        }

        // If this line has choices, stop automatic flow
        if (line.choices != null && line.choices.Count > 0)
        {
            ShowChoices(line.choices);
        }
    }

    public void ShowChoices(List<DialogueChoice> choices)
    {
        isDialogueActive = false;
        choicesPanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i].choiceText;

                int choiceIndex = i; // Needed to avoid closure issue
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() =>
                {
                    choicesPanel.SetActive(false);
                    choices[choiceIndex].onChoose.Invoke();
                    isDialogueActive = true;
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
        isDialogueActive = false;
    }
}
