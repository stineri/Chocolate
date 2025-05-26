using UnityEngine;
using System.Collections.Generic;

public class TriggerDialogueVendor : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public AudioClip vendorAngryClip;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartVendorDialogue();
        }
    }

    void StartVendorDialogue()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Vendor", "Hoy! Alis diyan! Wag mong guluhin ang paninda ko!", vendorAngryClip)
        };

        dialogueManager.StartDialogue(lines);
    }
}
