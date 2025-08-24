using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<ClientQuest> originalInfo = new List<ClientQuest>();
    [SerializeField] List<ClientQuest> currentInfo = new List<ClientQuest>();

    // Called the first time a client appears
    public void SetOriginalInfo()
    {

    }

    // Called every time the client appears, except for the first one
    public void SetQuestInfo()
    {

    }

    public void CheckQuest()
    {

    }
}

[System.Serializable]
public class ClientQuest
{
    [Header("Quest State")]
    public QuestState currentQuestState;
    public enum QuestState
    {
        NotStarted,
        Ongoing,
        GoodDeal,
        NeutralDeal,
        BadDeal
    }

    [Header("Visual")]
    public string currentName;
    public Sprite eye, head, teeth, outline;
    public Sprite currentFullSprite;

    [Header("Status")]
    [Range(-4, 3)] public float currentSatisfaction;
    [Range(0, 1)] public float currentResolution;
    public int cash;
    public List<ScriptableObject> possessions = new List<ScriptableObject>();

    [Header("Dialogue")]
    public string nextEncounter; // _dialogueManager.currentEncounter = nextEncounter;
}
