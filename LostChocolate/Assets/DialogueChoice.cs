using UnityEngine;
using System;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public Action onChoose;

    public DialogueChoice(string text, Action callback)
    {
        choiceText = text;
        onChoose = callback;
    }
}
