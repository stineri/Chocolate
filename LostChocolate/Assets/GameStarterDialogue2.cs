using UnityEngine;
using System.Collections.Generic;

public class GameStarterDialogue2 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Optional voice clips
    public AudioClip line1Clip2;


    void Start()
    {
        List<DialogueLine> lines = new List<DialogueLine>()
        {
            new DialogueLine("Choco", "Parang delikado rito ah…", line1Clip2)

        };

        dialogueManager.StartDialogue(lines);
    }
}
