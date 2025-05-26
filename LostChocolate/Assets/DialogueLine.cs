using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string text;
    public AudioClip voiceClip;
    public List<DialogueChoice> choices;
    public float waitTime = 0f; // Optional delay before auto-advance

    public DialogueLine(string speakerName, string text, AudioClip voiceClip = null, List<DialogueChoice> choices = null, float waitTime = 0f)
    {
        this.speakerName = speakerName;
        this.text = text;
        this.voiceClip = voiceClip;
        this.choices = choices;
        this.waitTime = waitTime;
    }
}
