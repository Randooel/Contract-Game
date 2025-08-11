using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

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
    [SerializeField] private List<ContractAssets> assets = new List<ContractAssets>();
    [SerializeField] private PlayerDebt _playerDebt;

    void Start()
    {
        currentState = State.ChitChat;

        //_contract.SetActive(false);

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

        HideContract();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SwitchState(State.Conclusion);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            assets[0].successStamp.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            assets[0].failStamp.SetActive(true);
        }
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
        HideContract();

        HandleEntrance();

        // Click contract button event logic to change state to negotiation
    }

    private void HandleNegotiation()
    {
        assets[0].contract.SetActive(true);
    }

    private void HandleConclusion()
    {
        assets[0].signature.SetActive(true);

        if (_currentClient.clientSatisfaction > 0)
        {
            assets[0].successStamp.gameObject.SetActive(true);
            assets[0].successStampVFX.Play();
        }
        else if((_currentClient.clientSatisfaction < 0))
        {
            assets[0].contract.SetActive(true);

            assets[0].failStamp.gameObject.SetActive(true);
            assets[0].failStampVFX.Play();
        }
    }

    private void HideContract()
    {
        foreach(var asset in assets)
        {
            asset.contract.SetActive(false);
            asset.signature.SetActive(false);

            asset.failStamp.SetActive(false);
            asset.successStamp.SetActive(false);

            asset.successStampVFX.Stop();
            asset.failStampVFX.Stop();
        }
    }

    public IEnumerator WaitToHideContract()
    {
        yield return new WaitForSeconds(1f);

        HideContract();
    }
}

[System.Serializable]
public class ContractAssets
{
    public GameObject contract;
    public GameObject signature;

    public GameObject successStamp;
    public GameObject failStamp;

    public VisualEffect successStampVFX;
    public VisualEffect failStampVFX;
}