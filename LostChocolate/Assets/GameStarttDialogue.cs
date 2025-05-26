using UnityEngine;
using System.Collections.Generic;

public class GameStartDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Optional voice clips
    public AudioClip line1Clip0;
    public AudioClip line1Clip;
    public AudioClip line2Clip;
    


    void Start()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Tip", "Click Enter close dialogue"),
            new DialogueLine("Choco", "Buti nakaligtas ako... pero saan na ko pupunta ngayon?", line1Clip0),
            new DialogueLine("Choco", "Ay, ang dami palang gala dito sa kalsada...", line1Clip),
            new DialogueLine("Choco", "Kailangan ko talagang mag-ingat.", line2Clip)
           
        };

        dialogueManager.StartDialogue(lines);
    }
}
