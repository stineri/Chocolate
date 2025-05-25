using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    public string text;
    public AudioClip voiceClip; // Optional: include this if you're using voice

    public DialogueLine(string speakerName, string text, AudioClip voiceClip = null)
    {
        this.speakerName = speakerName;
        this.text = text;
        this.voiceClip = voiceClip;
    }
}
