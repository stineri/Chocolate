using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string text;
    public AudioClip voiceClip;
    public List<DialogueChoice> choices;

    public DialogueLine(string speakerName, string text, AudioClip voiceClip = null, List<DialogueChoice> choices = null)
    {
        this.speakerName = speakerName;
        this.text = text;
        this.voiceClip = voiceClip;
        this.choices = choices;
    }
}
