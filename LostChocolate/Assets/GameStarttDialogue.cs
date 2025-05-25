using UnityEngine;
using System.Collections.Generic;

public class GameStartDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Optional voice clips
    public AudioClip line1Clip;
    public AudioClip line2Clip;
    public AudioClip line4Clip;


    void Start()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Choco", "Ay, ang dami palang gala dito sa kalsada...", line1Clip),
            new DialogueLine("Choco", "Kailangan ko talagang mag-ingat.", line2Clip),
            new DialogueLine("Tip", "Pindutin ang E para magtago sa mga basurahan,\njeep, o kahon para ‘di ka makita ng mga gala!"),
            new DialogueLine("Choco", "Ah, pwede palang magtago dito… \npara ‘di nila ako makita.", line4Clip)
        };

        dialogueManager.StartDialogue(lines);
    }
}
