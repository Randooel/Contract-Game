using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        /*
        var vitamin = originalInfo[index].possessions[0];
        var c = _currentClient;

        // Visual
        vitamin.currentName = c.clientName;
        //vitamin.eye = c.;

        // Status
        vitamin.currentSatisfaction = c.satisfaction;
        vitamin.currentResolution = c.resolution;

        // Objective
        //vitamin.currentObjectiveDescription = c.objectiveDescription;

        // Property
        //vitamin.currentCash = c.clientCash;
        //vitamin.currentObjectiveItem = c.obj

        index++;
        */
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

// [x]
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

    // Insert possessionList here for Original Info and Current Info


    [Header("Dialogue")]
    public string nextEncounter; // _dialogueManager.currentEncounter = nextEncounter;
}
