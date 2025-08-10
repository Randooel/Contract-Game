using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Player References")]
    private PlayerResponses _playerResponses;
    private int _response;

    [Header("Client References")]
    private ClientManager _clientManager;
    private CurrentClient _currentClient;

    [Header("Dialogue Group indexes")]
    [SerializeField] public int currentLine = 0;
    [SerializeField] public int currentDialogueGroup = 0;
    [Range(0, 5)] public int maxDialogueGroup;
    [SerializeField] public int currentEncounter;

    /*
    [Header("Current Client Variables")]
    int currentClientIndex;
    int currentDialogueIndex;
    bool shouldSkipDialogue;
    */

    private void Start()
    {
        _playerResponses = FindObjectOfType<PlayerResponses>();

        _clientManager = FindObjectOfType<ClientManager>();
        _currentClient = FindObjectOfType<CurrentClient>();
    }

    private void Update()
    {
        // Player's input
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            currentLine++;

            if(currentLine < _currentClient.lines.Count)
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

        foreach (var line in _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].clientLines)
        {
            _currentClient.lines.Add(line);
        }
    }

    public void ClearClientLines()
    {
        _currentClient.lines.Clear();
        currentLine = 0;
    }


    // Player Responses Functions
    public void SetPlayerResponses()
    {
        ClearPlayerResponses();

        if(_clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].playerResponses != null)
        {
            foreach (var response in _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].playerResponses)
            {
                _playerResponses.responsesLines.Add(response);
            }
        }
        else
        {
            ClearPlayerResponses();
        }
    }

    public void ClearPlayerResponses()
    {
        _playerResponses.responsesLines.Clear();
    }

    private void CheckQuestion()
    {
        if(currentLine == _currentClient.lines.Count)
        {
            _playerResponses.ShowResponses();
        }
        else return;
    }

    // Called by buttons
    public void OnResponseChosen()
    {
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if(buttonName == "Response 3")
        {
            _response = 0;
        }

        else if(buttonName == "Response 2")
        {
            _response = 1;
        }

        else if(buttonName == "Response 1")
        {
            _response = 2;
        }

        HandleResponse();

        _playerResponses.HideResponses();

        CheckNextDialogue();
    }

    private void HandleResponse()
    {
        _currentClient.clientSatisfaction += _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter]
            .dialogueGroups[currentDialogueGroup].answerSatisfaction[_response];

        if (_clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].
            dialogueGroups[currentDialogueGroup].answerSatisfaction[_response] > 0)
        {
            // Happy reaction
            _currentClient.reactions[0].SetActive(true);
        }
        else
        {
            // Angry reaction
            _currentClient.reactions[1].SetActive(true);
        }

        StartCoroutine(ReactionAnim());
    }

    private void CheckNextDialogue()
    {
        // Add null verification
        // Add verification of elements in the current group. If it is null, them
        // go to the next group. If the next group is null or higher than the
        // clientMaxLineGroup, than end conversation

        // If there's more elements in the current group, go to the next
        if (_clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].isExitDialogue == true)
        {
            Debug.Log("Exit");
            Exit();
        }
        else if (currentDialogueGroup + 1 <= maxDialogueGroup)
        {
            if(_clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].responseIndex == null)
            {
                currentDialogueGroup++;

                SetClientLines();
                SetPlayerResponses();

                _currentClient.Speak();
            }
            else
            {
                // implement jump to dialogue logic here OR on the HandleResponse function
            }
            
        }
        else
        {
            _currentClient.StopTalk();

            ClearClientLines();
            ClearPlayerResponses();
        }
    }

    private void Exit()
    {
        Debug.Log("Exit");
        _currentClient.PlayLeave();
    }

    private IEnumerator ReactionAnim()
    {
        yield return new WaitForSeconds(1f);

        foreach(var reaction in _currentClient.reactions)
        {
            reaction.SetActive(false);
        }
    }
}