using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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
    private ContractManager _contractManager;
    [SerializeField] private PlayerDebt _playerDebt;

    void Start()
    {
        currentState = State.ChitChat;

        // References check
        _contractManager ??= FindObjectOfType<ContractManager>();
        if( _contractManager == null )
        {
            Debug.LogWarning("ContractManager not found!");
        }

        _currentClient ??= FindObjectOfType<CurrentClient>();
        if (_currentClient == null)
        {
            Debug.LogWarning("CurrentClient not found!");
        }
        
        _clientManager ??= FindObjectOfType<ClientManager>();
        if(_clientManager == null)
        {
            Debug.LogWarning("ClientManager not found!");
        }

        _queueSystem ??= FindObjectOfType<QueueManager>();
        if(_queueSystem == null)
        {
            Debug.LogWarning("QueueSyestem not found!");
        }

        _contractManager.HideContract();
    }

    private void Update()
    {
        
    }

    public void SwitchState(State state)
    {
        currentState = state;

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
        _contractManager.HideContract();

        HandleEntrance();

        // Click contract button event logic to change state to negotiation
    }

    private void HandleNegotiation()
    {
        _contractManager.ShowContract();
    }

    private void HandleConclusion()
    {
        _contractManager.ShowSignature();

        if (_currentClient.clientSatisfaction > 0)
        {
            StartCoroutine(WaitToPlayStampVFX(1));
            
        }
        else if((_currentClient.clientSatisfaction < 0))
        {
            StartCoroutine(WaitToPlayStampVFX(-1));
        }
    }

    public IEnumerator WaitToHideContract()
    {
        yield return new WaitForSeconds(1f);

        _contractManager.HideContract();
    }

    public IEnumerator WaitToPlayStampVFX(int stamp)
    {
        yield return new WaitForSeconds(1f);

        if(stamp < 0)
        {
            _contractManager.PlaySuccessVFX();
        }
        else
        {
            _contractManager.PlayFailVFX();
        }
    }
}