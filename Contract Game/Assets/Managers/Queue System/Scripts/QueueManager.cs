using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Header("Scripts References")]
    private ClientManager _clientManager;
    private DialogueManager _dialogueManager;
    private ContractManager _contractManager;

    [Header("Served Clients")]
    public List<ClientProfileSO> servedClients;
    // Add a List of possible clients for each day

    [Header("Day System")]
    [SerializeField] private int _currentDay = 1;
    [SerializeField] private TextMeshProUGUI _dayText;


    void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _contractManager = FindObjectOfType<ContractManager>();

        StartCoroutine(WaitToAddClient());

        // Insert random queue logic here, without repetition
        // And SetClient logic (and remove the one on the ClientManager script)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1) && _clientManager.canCallNextClient == true)
        {
            _clientManager.CallNextClient();
            _dialogueManager.ClearClientLines();
            _dialogueManager.ClearPlayerResponses();
            _dialogueManager.currentLine = 0;

            _contractManager.HideContract();

            AddServedClient();
        }
        else return;
    }

    /*
    public void NextRandomClient()
    {
        _clientManager.GenerateClient();
    }
    */

    public void AddServedClient()
    {
        //_clientManager.ChooseRandomProfile();

        servedClients.Add(_clientManager.profileSO[_clientManager.currentProfile]);
    }

    // Insert logic to not call client's fully served and update their quests based on the deal

    public void UpdatCharacterDialogue()
    {
        
    }

    private IEnumerator WaitToAddClient()
    {
        yield return new WaitForSeconds(0.05f);

        AddServedClient();
    }
}
