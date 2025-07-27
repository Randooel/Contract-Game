using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class ClientManager : MonoBehaviour
{
    [Header("Player Reference")]
    private PlayerResponses _playerResponse;

    [Header("DialogueManager Reference")]
    private DialogueManager _dialogueManager;

    [Header("Client Reference")]
    CurrentClient _currentClient;
    [SerializeField] SpriteRenderer _clientEyes;
    [SerializeField] SpriteRenderer _clientHead;
    [SerializeField] SpriteRenderer _clientTeeth;
    [SerializeField] SpriteRenderer _clientOutline;

    [Header("SO References")]
    public ClientNameSO clientNameSO;
    public ClientSpritesListSO spritesListSO;
    public ClientObjectiveSO objectivesSO;
    public ClientGreetingLinesSO personalitySO;
    public ClientProfileSO clientSO;

    public List<ClientProfileSO> profileSO = new List<ClientProfileSO>();
    public int currentProfile;

    [Header("Dialogue")]
    [SerializeField] protected GameObject _clientDialgoueBox;
    [SerializeField] protected ClientLinesListSO _clientLinesList;

    [Header("Next Client")]
    public bool canCallNexClient = true;

    void Start()
    {
        if(_currentClient == null)
        {
            _currentClient = FindObjectOfType<CurrentClient>();
        }

        if(_playerResponse == null)
        {
            _playerResponse = FindObjectOfType<PlayerResponses>();

            if(_playerResponse == null)
            {
                Debug.LogWarning("PlayPlayerResponse not found!");
            }
        }

        _dialogueManager = FindObjectOfType<DialogueManager>();

        ChooseRandomProfile();
        //GenerateClient();
    }

    void Update()
    {
        
    }

    public void CallNextClient()
    {
        canCallNexClient = false;

        _currentClient.lines.Clear();
        _currentClient.currentLine = 0;

        _playerResponse.HideResponses();

        _currentClient.PlayLeave();

        //DOTween.Kill(this.gameObject);

        DOVirtual.DelayedCall(1f, () =>
        {
            //GenerateClient();
            ChooseRandomProfile();

            canCallNexClient = true;
        });
    }


    // PREDETERMINED CLIENT PROFILES
    // Comment this script after the QueueManager profile pick logic is implemented
    public void ChooseRandomProfile()
    {
        int rand = Random.Range(0, profileSO.Count);
        currentProfile = rand;

        SetProfile();
    }

    public void SetProfile()
    {
        _currentClient.clientName = profileSO[currentProfile].clientName;
        _currentClient.clientTextName.text = profileSO[currentProfile].clientName;
        _clientEyes.sprite = profileSO[currentProfile].eyesSprite;
        _clientHead.sprite = profileSO[currentProfile].headSprite;
        _clientTeeth.sprite = profileSO[currentProfile].teethSprite;
        _clientOutline.sprite = profileSO[currentProfile].outlineSprite;
        RandomizeColor();
        //currentClient.clientObjectives = profileSO[currentProfile].objective;
        _currentClient.clientResolution = profileSO[currentProfile].resolution;
        _currentClient.clientCash = profileSO[currentProfile].cash;

        _dialogueManager.SetClientLines();
        _dialogueManager.SetPlayerResponses();

        _dialogueManager.clientMaxDialogueGroup = profileSO[currentProfile].dialogueGroups.Count;
        _dialogueManager.clientCurrentDialogueGroup = 0;

        _currentClient.PlayEntrance();
    }


    // RANDOM CLIENTS
    /*
    public void GenerateClient()
    {
        _currentClient.clientTextName.text = clientNameSO.GetRandomName();
        //currentClient.clientSprite = spritesSO.GetRandomSprite();
        RandomizeSprite();
        //objectivesSO.GetRandomObjective();
        RandomizeLines();
        RandomizeResolution();
        RandomizeCash();
        RandomizeColor();

        _currentClient.PlayEntrance();
    }

    void RandomizeLines()
    {
        int rand = Random.Range(0, _clientLinesList.linesList.Count);
        var selectedLinesList = _clientLinesList.linesList[rand];
        _currentClient.clientPersonality = selectedLinesList;

        _currentClient.lines.Clear();
        _currentClient.currentLine = 0;

        foreach(var line in selectedLinesList.lines)
        {
            _currentClient.lines.Add(line);
        }
    }

    void RandomizeSprite()
    {
        //int rand = Random.Range(0, spritesListSO.clientSpritesListSOs.Count);
        //currentClient.clientSprite = spritesListSO.clientSpritesListSOs.ClientSpriteSO.sprites[rand];
        

        int rand = Random.Range(0, spritesListSO.clientSpritesListSO.Count);
        var selectedSO = spritesListSO.clientSpritesListSO[rand];

        _clientEyes.sprite = selectedSO.clientEyes;
        _clientHead.sprite = selectedSO.clientHead;
        _clientTeeth.sprite = selectedSO.clientTeeth;
        _clientOutline.sprite = selectedSO.clientOutline;
    }

    void RandomizeCash()
    {
        int rand = Random.Range(0, 9999);
        _currentClient.clientCash = rand;
    }

    void RandomizeResolution()
    {
        float rand = Random.Range(0, 1);
        _currentClient.clientResolution = rand;
    }
    */
    void RandomizeColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        _clientHead.color = randomColor;
    }
}