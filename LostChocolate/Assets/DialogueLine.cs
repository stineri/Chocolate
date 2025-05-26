using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string text;
    public AudioClip voiceClip;
    public List<DialogueChoice> choices;
    public float autoAdvanceDelay;

    public DialogueLine(string name, string text, AudioClip clip = null, List<DialogueChoice> choices = null, float delay = 0f)
    {
        speakerName = name;
        this.text = text;
        voiceClip = clip;
        this.choices = choices;
        autoAdvanceDelay = delay;
    }
}
