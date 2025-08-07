using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Player References")]
    private PlayerResponses _playerResponses;

    [Header("Client References")]
    private ClientManager _clientManager;
    private CurrentClient _currentClient;

    [Header("Dialogue Group indexes")]
    [SerializeField] public int clientCurrentLine = 0;
    [SerializeField] public int clientCurrentDialogueGroup = 1;
    [Range(0, 5)] public int clientMaxDialogueGroup;

    private void Start()
    {
        _playerResponses = FindObjectOfType<PlayerResponses>();

        _clientManager = FindObjectOfType<ClientManager>();
        _currentClient = FindObjectOfType<CurrentClient>();
    }

    private void Update()
    {
        // Player's input
        if(Input.GetKeyDown(KeyCode.Space))
        {
            clientCurrentLine++;

            if(clientCurrentLine < _currentClient.lines.Count)
            {
                _currentClient.Speak();
            }
            else
            {
                CheckQuestion();
            }
        }
    }


    // Client Lines Functions
    public void SetClientLines()
    {
        ClearClientLines();

        foreach (var line in _clientManager.profileSO[_clientManager.currentProfile].dialogueGroups[clientCurrentDialogueGroup].clientLines)
        {
            _currentClient.lines.Add(line);
        }
    }

    public void ClearClientLines()
    {
        _currentClient.lines.Clear();
        clientCurrentLine = 0;
    }


    // Player Responses Functions
    public void SetPlayerResponses()
    {
        ClearPlayerResponses();

        foreach (var response in _clientManager.profileSO[_clientManager.currentProfile].dialogueGroups[clientCurrentDialogueGroup].playerResponses)
        {
            _playerResponses.responsesLines.Add(response);
        }
    }

    public void ClearPlayerResponses()
    {
        _playerResponses.responsesLines.Clear();
    }

    private void CheckQuestion()
    {
        if(clientCurrentLine == _currentClient.lines.Count)
        {
            _playerResponses.ShowResponses();
        }
        else return;
    }

    // Called by buttons
    public void OnResponseChosen3()
    {
        _playerResponses.HideResponses();

        // Calculates client's satisfaction to the response chosen
        _currentClient.clientSatisfaction += _clientManager.profileSO[_clientManager.currentProfile]
            .dialogueGroups[clientCurrentDialogueGroup].answerSatisfaction3;

        // Add null verification
        // Add verification of elements in the current group. If it is null, them
        // go to the next group. If the next group is null or higher than the
        // clientMaxLineGroup, than end conversation

        // If there's more elements in the current group, go to the next
        if (clientCurrentDialogueGroup + 1 < clientMaxDialogueGroup)
        {
            clientCurrentDialogueGroup++;

            SetClientLines();
            SetPlayerResponses();

            _currentClient.Speak();
        }
        else
        {
            _currentClient.StopTalk();

            ClearClientLines();
            ClearPlayerResponses();
        }
    }

    public void OnResponseChosen2()
    {
        _playerResponses.HideResponses();

        // Calculates client's satisfaction to the response chosen
        _currentClient.clientSatisfaction += _clientManager.profileSO[_clientManager.currentProfile]
            .dialogueGroups[clientCurrentDialogueGroup].answerSatisfaction2;

        // Add null verification
        // Add verification of elements in the current group. If it is null, them
        // go to the next group. If the next group is null or higher than the
        // clientMaxLineGroup, than end conversation

        // If there's more elements in the current group, go to the next
        if (clientCurrentDialogueGroup + 1 < clientMaxDialogueGroup)
        {
            clientCurrentDialogueGroup++;

            SetClientLines();
            SetPlayerResponses();

            _currentClient.Speak();
        }
        else
        {
            _currentClient.StopTalk();

            ClearClientLines();
            ClearPlayerResponses();
        }
    }

    public void OnResponseChosen1()
    {
        _playerResponses.HideResponses();

        // Calculates client's satisfaction to the response chosen
        _currentClient.clientSatisfaction += _clientManager.profileSO[_clientManager.currentProfile]
            .dialogueGroups[clientCurrentDialogueGroup].answerSatisfaction1;

        // Add null verification
        // Add verification of elements in the current group. If it is null, them
        // go to the next group. If the next group is null or higher than the
        // clientMaxLineGroup, than end conversation

        // If there's more elements in the current group, go to the next
        if (clientCurrentDialogueGroup + 1 < clientMaxDialogueGroup)
        {
            clientCurrentDialogueGroup++;

            SetClientLines();
            SetPlayerResponses();

            _currentClient.Speak();
        }
        else
        {
            _currentClient.StopTalk();

            ClearClientLines();
            ClearPlayerResponses();
        }
    }
}