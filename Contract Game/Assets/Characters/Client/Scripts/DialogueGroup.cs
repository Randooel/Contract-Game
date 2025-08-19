using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTextEffects.Editor.MyBoxCopy.Attributes;

[System.Serializable]
public class DialogueGroup
{
    [Header("Dialogue Info")]
    public string dialogueTag;

    //public bool isExitDialogue;

    public enum DialogueState
    {
        Chitchat,
        Negotiation,
        Conclusion
    }
    public DialogueState currentDialogueState;

    [Header("Dialogue")]
    [TextArea(1, 3)]
    public List<string> clientLines;

    public List<PlayerResponsesList> playerResponses = new List<PlayerResponsesList>();
}

[System.Serializable]
public class PlayerResponsesList
{
    [TextArea(1, 3)]
    public string responseLine;

    [Space(5)]
    [Range(-3, 3)] public int answerSatisfaction;

    [Space(5)]
    public bool skipToDialogueX;

    [ConditionalField(nameof(skipToDialogueX), false)]
    [Space(5)]
    public string nextDialogueTag;
}