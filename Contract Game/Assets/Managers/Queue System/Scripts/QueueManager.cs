using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Header("Scripts References")]
    private ClientManager _clientManager;
    private DialogueManager _dialogueManager;

    [Header("Served Clients")]
    public List<ClientProfileSO> _profiles;


    void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();

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
        }
        else return;
    }

    /*
    public void NextRandomClient()
    {
        _clientManager.GenerateClient();
    }
    */

    public void NextRandomProfile()
    {
        _clientManager.ChooseRandomProfile();
        _profiles.Add(_clientManager.profileSO[_clientManager.currentProfile]);
    }

    // Insert logic to not call client's fully served and update their quests based on the deal

    public void UpdatCharacterDialogue()
    {
        
    }
}
