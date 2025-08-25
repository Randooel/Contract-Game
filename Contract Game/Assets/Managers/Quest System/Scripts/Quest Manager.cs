using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Other Scripts")]
    private QueueManager _queueManager;
    private CurrentClient _currentClient;

    [Header("Lists")]
    [SerializeField] List<ClientQuest> originalInfo = new List<ClientQuest>();
    [SerializeField] List<ClientQuest> currentInfo = new List<ClientQuest>();

    private int index;

    private void Start()
    {
        _queueManager = FindObjectOfType<QueueManager>();
        _currentClient = FindObjectOfType<CurrentClient>();

        SetListsSizes();
    }

    // Called the first time a client appears
    public void SetOriginalInfo()
    {
        var vitamin = originalInfo[index].possessions[0];
        var c = _currentClient;

        // Visual
        vitamin.currentName = c.clientName;
        //vitamin.eye = c.;

        // Status
        vitamin.currentSatisfaction = c.clientSatisfaction;
        vitamin.currentResolution = c.clientResolution;

        // Objective
        vitamin.currentObjectiveDescription = c.objectiveDescription;

        // Property
        vitamin.currentCash = c.clientCash;
        //vitamin.currentObjectiveItem = c.obj

        index++;
    }

    // Called every time the client appears, except for the first one
    public void SetQuestInfo()
    {

    }

    public void CheckQuest()
    {

    }

    private void RefreshList()
    {

    }

    private void SetListsSizes()
    {
        originalInfo = new List<ClientQuest>(new ClientQuest[_queueManager.profilesPerDay.Count]);
        currentInfo = new List<ClientQuest>(new ClientQuest[originalInfo.Count]);
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

    public List<ClientPossessions> possessions = new List<ClientPossessions>();


    [Header("Dialogue")]
    public string nextEncounter; // _dialogueManager.currentEncounter = nextEncounter;
}

[System.Serializable]
public class ClientPossessions
{
    [Header("Visual")]
    public string currentName;
    public Sprite eye, head, teeth, outline;
    [SerializeField] SpriteRenderer _clientEyes, _clientHead, _clientTeeth, _clientOutline;
    public Sprite currentFullSprite;

    [Header("Status")]
    [Range(-4, 3)] public float currentSatisfaction;
    [Range(0, 1)] public float currentResolution;

    [Header("Objective")]
    public string currentObjectiveDescription;
    public ClientProfileSO currentObjectiveCharacter;
    public ItemSO currentObjectiveItem;

    [Header("Property")]
    public int currentCash;
    public List<ScriptableObject> items = new List<ScriptableObject>();
}
