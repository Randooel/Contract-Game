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

    [Header("Day System")]
    [SerializeField] public int currentDay = 1;
    [SerializeField] private TextMeshProUGUI _dayText;

    [Header("Served Clients")]
    public List<ClientProfileSO> servedClients;

    [Header("Client Lists")]
    public List<ProfileList> profilesPerDay = new List<ProfileList>();



    void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _contractManager = FindObjectOfType<ContractManager>();

        _dayText.text = "Day: " + currentDay + "/3";
        UpdateClientManagerProfileList();

        //StartCoroutine(WaitToAddClient());

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
        servedClients.Add(_clientManager.profileSO[_clientManager.currentProfile]);
    }

    public void CheckServed(int rand)
    {
        bool wasServed = false;

        foreach (var profile in servedClients)
        {
            if (_clientManager.profileSO[rand] == profile)
            {
                wasServed = true;
                break;
            }
        }

        if (!wasServed)
        {
            var index = currentDay - 1;
            _clientManager.currentProfile = rand;
            _clientManager.SetProfile();
            AddServedClient();
        }
        else
        {
            _clientManager.ChooseRandomProfile();
        }
    }

    public void UpdateClientManagerProfileList()
    {
        _clientManager.profileSO.Clear();

        foreach(var profile in profilesPerDay[currentDay - 1].profileList)
        {
            _clientManager.profileSO.Add(profile);
        }
    }

    public void UpdatCharacterDialogue()
    {
        
    }

    /*
    private IEnumerator WaitToAddClient()
    {
        yield return new WaitForSeconds(0.05f);

        AddServedClient();
    }
    */

    public void HandleNextDay()
    {
        currentDay++;
        servedClients.Clear();
        UpdateClientManagerProfileList();

        // JUST FOR TESTS
        _clientManager.CallNextClient();
    }
}

[System.Serializable]
public class ProfileList
{
    public string dayIndex;
    public List<ClientProfileSO> profileList = new List<ClientProfileSO>();
}