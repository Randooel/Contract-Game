using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTextEffects;

public class DialogueManager : MonoBehaviour
{
    [Header("Negotiation System Reference(s)")]
    private NegotiationManager _negotiationManager;

    [Header("Player References")]
    private PlayerResponses _playerResponses;
    private int _responseIndex;

    [Header("Client References")]
    private ClientManager _clientManager;
    private CurrentClient _currentClient;

    [Header("Dialogue Group indexes")]
    [SerializeField] public int currentLine = 0;
    [SerializeField] public int currentDialogueGroup = 0;
    [Range(0, 5)] public int maxDialogueGroup;
    [SerializeField] public int currentEncounter;

    [Header("Visual")]
    [SerializeField] TextEffect textEffect;

    private void Start()
    {
        _negotiationManager = FindObjectOfType<NegotiationManager>();

        _playerResponses = FindObjectOfType<PlayerResponses>();

        _clientManager = FindObjectOfType<ClientManager>();
        _currentClient = FindObjectOfType<CurrentClient>();

        textEffect = FindObjectOfType<TextEffect>();
    }

    private void Update()
    {
        // Player's input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)
            && _clientManager.canCallNextClient 
            && _playerResponses.isReponseActive == false)
        {
            currentLine++;

            if (currentLine < _currentClient.lines.Count)
            {
                _currentClient.Speak();
            }
            else
            {
                CheckQuestion();
            }
        }
        else return;
    }

    // Client Lines Functions
    public void SetClientLines()
    {
        var cLines = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].clientLines;
        var cDialogueState = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].currentDialogueState;

        ClearClientLines();

        foreach (var line in cLines)
        {
            _currentClient.lines.Add(line);
        }

        // This is a cast. It can be used only when both enums have the same values
        _negotiationManager.SwitchState((NegotiationManager.State)cDialogueState);
    }

    public void ClearClientLines()
    {
        _currentClient.lines.Clear();
        currentLine = 0;
    }


    // Player Responses Functions
    public void SetPlayerResponses()
    {
        var playerResponses = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].playerResponses;

        ClearPlayerResponses();

        if(playerResponses != null && playerResponses.Count > 0)
        {
            /*
            foreach (var response in playerResponses)
            {
                _playerResponses.responsesLines.Add(response);
            }
            */
            for (int i = 0; i < playerResponses.Count; i++)
            {
                _playerResponses.responsesLines.Add(playerResponses[i].responseLine);
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
        var pResponses = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups[currentDialogueGroup].playerResponses;

        //if (isExitDialogue)
        if (pResponses?.Count > 0)
        {
            _playerResponses.ShowResponses();
        }
        else
        {
            Exit();
        }
    }

    // Called by buttons
    public void OnResponseChosen()
    {
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if(buttonName == "Response 3")
        {
            _responseIndex = 0;
        }

        else if(buttonName == "Response 2")
        {
            _responseIndex = 1;
        }

        else if(buttonName == "Response 1")
        {
            _responseIndex = 2;
        }

        HandleResponse();

        _playerResponses.HideResponses();

        CheckNextDialogue();
    }

    private void HandleResponse()
    {
        var answerSatisfaction = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter]
            .dialogueGroups[currentDialogueGroup].playerResponses[_responseIndex].answerSatisfaction;

        _currentClient.satisfaction += answerSatisfaction;

        if (answerSatisfaction > 0)
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
        var nextDialogue = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].
            dialogueGroups[currentDialogueGroup].playerResponses[_responseIndex].nextDialogueTag;


        if (currentDialogueGroup + 1 < maxDialogueGroup)
        {
            var shouldSkipToDialogueX = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].
                    dialogueGroups[currentDialogueGroup].playerResponses[_responseIndex].skipToDialogueX;

            if (shouldSkipToDialogueX)
            {
                string nextDialogueTag = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].
                    dialogueGroups[currentDialogueGroup].playerResponses[_responseIndex].nextDialogueTag;

                var dialogueGroups = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups;

                int nextIndex = dialogueGroups.FindIndex(d => d.dialogueTag == nextDialogueTag);

                // The list function 'FindIndex' returns '-1' when the it doesn't find the list element
                // So, in this context, -1 = null
                if (nextIndex != -1)
                {
                    currentDialogueGroup = nextIndex;

                    SetClientLines();
                    SetPlayerResponses();

                    _currentClient.Speak();
                }
                else
                {
                    Debug.LogError("Dialogue with tag " + nextDialogueTag + " not found");
                }
            }
            else
            {
                currentDialogueGroup++;

                SetClientLines();
                SetPlayerResponses();

                _currentClient.Speak();
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
        var dialogueGroups = _clientManager.profileSO[_clientManager.currentProfile].encounters[currentEncounter].dialogueGroups;

        int nextIndex = dialogueGroups.FindIndex(d => d.dialogueTag == "EXIT");

        if(nextIndex != -1)
        {
            currentDialogueGroup = nextIndex;
        }
        else
        {
            Debug.LogError("Dialogue Element with tag " + nextIndex + " not found");
        }

        currentDialogueGroup = 0;
        _clientManager.CallNextClient();

        _negotiationManager.StartCoroutine(_negotiationManager.WaitToHideContract());
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