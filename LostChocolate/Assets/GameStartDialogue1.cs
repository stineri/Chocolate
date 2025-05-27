using UnityEngine;
using System.Collections.Generic;

public class GameStartDialogue1 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Optional voice clips
    public AudioClip line1Clip1;
    

    void Start()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Tip", "Click Enter to close dialogue"),
            new DialogueLine("Choco", "Grabe, gutom na gutom ako... \nbaka may pagkain dito sa palengke.", line1Clip1)

        };

        dialogueManager.StartDialogue(lines);
    }
}
