using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public DialogueManager dialogueManager;

    // Optional: ensure it only triggers once
    private bool hasTriggered = false;

    // Audio clip example
    public AudioClip line2Clip;
    public AudioClip line4Clip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartEnemyDialogue2();
        }
    }

    void StartEnemyDialogue2()
    {
        List<DialogueLine> playerLines = new List<DialogueLine>()
        {
            new DialogueLine("Enemy Dog", "Hoy! May naligaw na aso dito! Bantayan natin ‘to, teritoryo namin ‘to!", line2Clip),
            new DialogueLine("Tip", "Pindutin ang E para magtago sa mga basurahan,\njeep, o kahon para ‘di ka makita ng mga gala!"),
            new DialogueLine("Choco", "Ah, pwede palang magtago dito… \npara ‘di nila ako makita.", line4Clip)

        };

        dialogueManager.StartDialogue(playerLines);
    }
}
