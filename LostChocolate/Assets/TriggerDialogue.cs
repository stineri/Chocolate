using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Voice Clips
    public AudioClip voiceClip1; // "Tama na yan!"
    public AudioClip voiceClip2; // "Wag niyong saktan ‘yung aso!"
    public AudioClip voiceClip3; // "Wala namang ginagawang masama!"
    public AudioClip voiceBread; // Tinapay line
    public AudioClip voiceOffer; // Gusto mo bang sumama
    public AudioClip voiceYes;   // Choco barks
    public AudioClip voiceNo;    // Choco sad
    public AudioClip voiceSad;   // Mang Luis goodbye

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartDialogueSequence();
        }
    }

    void StartDialogueSequence()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Mang Luis", "Tama na yan!", voiceClip1),
            new DialogueLine("Mang Luis", "Wag niyong saktan ‘yung aso!", voiceClip2),
            new DialogueLine("Mang Luis", "Wala namang ginagawang masama!", voiceClip3),
            new DialogueLine("Mang Luis", "Halika dito, ‘nak. Eto, tinapay oh. Kainin mo.", voiceBread),
            
            // Pause line - auto advances after 5 seconds
            new DialogueLine("", "...", null, null, 5f),

            new DialogueLine("Mang Luis", "Ang bait mo palang aso. Gusto mo bang sumama sa’kin?\nBibigyan kita ng pagkain, tahanan, at alaga.\nMalapit lang bahay ko. Ayos ba ’yon, ha?\nGusto mo bang sumama?",voiceOffer,
                new List<DialogueChoice>()
                {
                    new DialogueChoice("Oo po, gusto ko po.", OnChooseYes),
                    new DialogueChoice("Ayaw ko po, maglalakad pa po ako.", OnChooseNo)
                }
            )
        };

        dialogueManager.StartDialogue(lines);
    }

    void OnChooseYes()
    {
        List<DialogueLine> yesLines = new List<DialogueLine>()
        {
            new DialogueLine("Choco (tumahol nang masaya)", "Arf arf!", voiceYes)
            
        };

        dialogueManager.StartDialogue(yesLines);
    }

    void OnChooseNo()
    {
        List<DialogueLine> noLines = new List<DialogueLine>()
        {
            new DialogueLine("Choco", "Awuu...", voiceNo),
            new DialogueLine("Mang Luis", "Sige... ingat ka palagi, ‘nak.", voiceSad)
        };

        dialogueManager.StartDialogue(noLines);
    }
}
