using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Header("Scripts References")]
    private ClientManager _clientManager;
    private DialogueManager _dialogueManager;


    void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();

        // Insert random queue logic here, without repetition
        // And SetClient logic (and remove the one on the ClientManager script)
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _clientManager.canCallNextClient == true)
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
    }
}
