using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [TextArea(1, 3)]
    public List<string> playerResponses;

    [Header("Satisfaction")]
    [Range(-3, 3)] public List<int> answerSatisfaction = new List<int>();

    [Header("Jump to Dialogue")]
    public List<int> responseIndex = new List<int>();
    public List<string> nextDialogueElement = new List<string>();
}