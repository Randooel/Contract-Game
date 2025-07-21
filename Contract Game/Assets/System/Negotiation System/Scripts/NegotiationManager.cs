using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegotiationManager : MonoBehaviour
{
    [Header("Negotiation States")]
    public State currentState;
    public enum State
    {
        ChitChat,
        Negotiation,
        Conclusion
    }

    [Header("Client References")]
    [SerializeField] private CurrentClient _currentClient;
    [SerializeField] private ClientManager _clientManager;
    [SerializeField] private QueueManager _queueSystem;

    [Header("Interface References")]
    [SerializeField] private GameObject _contract;

    void Start()
    {
        currentState = State.ChitChat;

        _contract.SetActive(false);

        // References check
        if (_currentClient == null)
        {
            _currentClient = FindObjectOfType<CurrentClient>();

            if (_currentClient == null)
            {
                Debug.LogWarning("CurrentClient not found!");
            }
        }

        if(_clientManager == null)
        {
            _clientManager = FindObjectOfType<ClientManager>();

            if(_clientManager == null)
            {
                Debug.LogWarning("ClientManager not found!");
            }
        }

        if(_queueSystem == null)
        {
            _queueSystem = FindObjectOfType<QueueManager>();

            if(_queueSystem == null)
            {
                Debug.LogWarning("QueueSyestem not found!");
            }
        }

        if (_contract == null)
        {
            _contract = FindObjectOfType<GameObject>();

            if(_contract == null)
            {
                Debug.LogWarning("Contract not found!");
            }
        }
    }

    private void Update()
    {
        // Placeholder change to Negotiation State logic
        if(currentState == State.ChitChat)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentState = State.Negotiation;
                SwitchState();
            }
        }

        // Placeholder change to Conclusion State logic
        if(currentState == State.Negotiation)
        {
            if(Input.GetMouseButtonDown(1))
            {
                currentState = State.Conclusion;
                SwitchState();
            }
        }
    }

    public void SwitchState()
    {
        switch(currentState)
        {
            case State.ChitChat :
                HandleChitChat();
                break;
            case State.Negotiation :
                HandleNegotiation();
                break;
            case State.Conclusion :
                HandleConclusion();
                break;
            default:
                Debug.LogError("Undefined State");
                break;
        }
    }
    private void HandleEntrance()
    {
        //_queueSystem.NextRandomClient();
    }

    private void HandleChitChat()
    {
        _contract.SetActive(false);

        HandleEntrance();

        // Click contract button event logic to change state to negotiation
    }

    private void HandleNegotiation()
    {
        _contract.SetActive(true);
    }

    private void HandleConclusion()
    {
        _contract.SetActive(false);
    }
}
