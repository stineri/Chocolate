using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager2 : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject choicesPanel; // Optional
    public Button[] choiceButtons;  // Optional

    [Header("Audio")]
    public AudioSource audioSource;

    private Queue<DialogueLine> dialogueLines = new Queue<DialogueLine>();
    private bool isDialogueActive = false;
    private bool isWaitingForAudio = false;

    void Update()
    {
        bool choicePanelActive = choicesPanel != null && choicesPanel.activeSelf;

        if (isDialogueActive && dialoguePanel.activeSelf && !choicePanelActive && !isWaitingForAudio)
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

        // Stop any current audio
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        // If there's a voice clip, play and wait for it to finish
        if (line.voiceClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(line.voiceClip);
            isWaitingForAudio = true;
            Invoke(nameof(ShowNextLine), line.voiceClip.length);
            return;
        }

        // Show choices if present
        if (line.choices != null && line.choices.Count > 0 && choicesPanel != null && choiceButtons != null)
        {
            ShowChoices(line.choices);
            return;
        }

        // If autoAdvanceDelay is set, use it
        if (line.autoAdvanceDelay > 0f)
        {
            isDialogueActive = false;
            Invoke(nameof(ShowNextLine), line.autoAdvanceDelay);
            return;
        }

        // No audio, no choices, no delay → disappear after 2 seconds by default
        isDialogueActive = false;
        Invoke(nameof(ShowNextLine), 2f);
    }

    public void ShowChoices(List<DialogueChoice> choices)
    {
        isDialogueActive = false;
        isWaitingForAudio = false;

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
                    ShowNextLine();
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
        isWaitingForAudio = false;
    }
}
