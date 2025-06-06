using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject choicesPanel; // Optional
    public Button[] choiceButtons;  // Optional

    [Header("Player Control")]
    public PlayerMovement playerMovement; // Optional player control reference

    private Queue<DialogueLine> dialogueLines = new Queue<DialogueLine>();
    private bool isDialogueActive = false;

    void Update()
    {
        bool choicePanelActive = choicesPanel != null && choicesPanel.activeSelf;

        if (isDialogueActive && dialoguePanel.activeSelf && !choicePanelActive)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                ShowNextLine();
            }
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

        if (playerMovement != null)
            playerMovement.enabled = false;

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

        // Show choices if present and choicesPanel is assigned
        if (line.choices != null && line.choices.Count > 0 && choicesPanel != null && choiceButtons != null)
        {
            ShowChoices(line.choices);
            return;
        }

        // Auto-advance if delay is set
        if (line.autoAdvanceDelay > 0f)
        {
            isDialogueActive = false;
            Invoke(nameof(ShowNextLine), line.autoAdvanceDelay);
        }
    }

    public void ShowChoices(List<DialogueChoice> choices)
    {
        isDialogueActive = false;

        if (choicesPanel != null)
            choicesPanel.SetActive(true);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = choices[i].choiceText;

                int choiceIndex = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() =>
                {
                    if (choicesPanel != null)
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

        if (choicesPanel != null)
            choicesPanel.SetActive(false);

        isDialogueActive = false;

        if (playerMovement != null)
            playerMovement.enabled = true;
    }
}
