using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ClientManager : MonoBehaviour
{
    private QueueManager _queueManager;

    [Header("Player Reference")]
    private PlayerResponses _playerResponse;

    [Header("Negotiation Reference")]
    private NegotiationManager _negotiationManager;

    [Header("DialogueManager Reference")]
    private DialogueManager _dialogueManager;

    [Header("Client Reference")]
    CurrentClient _currentClient;
    [SerializeField] SpriteRenderer _clientEyes;
    [SerializeField] SpriteRenderer _clientHead;
    [SerializeField] SpriteRenderer _clientTeeth;
    [SerializeField] SpriteRenderer _clientOutline;
    [Space(5)]
    [SerializeField] Image _dialogueBoxBackground;

    [Header("SO References")]
    public ClientNameSO clientNameSO;
    public ClientSpritesListSO spritesListSO;
    public ClientObjectiveSO objectivesSO;
    /*
    public ClientGreetingLinesSO personalitySO;
    public ClientProfileSO clientSO;
    */

    [Space(10)]
    public List<ClientProfileSO> profileSO = new List<ClientProfileSO>();
    public int currentProfile;

    [Header("Dialogue")]
    [SerializeField] protected GameObject _clientDialgoueBox;
    [SerializeField] protected ClientLinesListSO _clientLinesList;

    [Header("Next Client")]
    public bool canCallNextClient = true;

    void Start()
    {
        _queueManager = FindObjectOfType<QueueManager>(); 

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


        _negotiationManager = FindObjectOfType<NegotiationManager>();

        ChooseRandomProfile();
        //GenerateClient();
    }

    void Update()
    {
        
    }

    public void CallNextClient()
    {
        if (canCallNextClient == true)
        {
            canCallNextClient = false;

            _currentClient.lines.Clear();
            _currentClient.currentLine = 0;

            _playerResponse.HideResponses();

            _currentClient.PlayLeave();

            //DOTween.Kill(this.gameObject);

            DOVirtual.DelayedCall(2f, () =>
            {
                canCallNextClient = true;
            });

            StartCoroutine(WaitToNextClient());
        }
        else return;
        
    }

    private IEnumerator WaitToNextClient()
    {
        yield return new WaitForSeconds(1f);

        ChooseRandomProfile();
    }


    // PREDETERMINED CLIENT PROFILES
    // Comment this script after the QueueManager profile pick logic is implemented
    public void ChooseRandomProfile()
    {
        bool wasServed = false;
        int rand = Random.Range(0, profileSO.Count);

        foreach (var profile in _queueManager.servedClients)
        {
            if(profileSO[rand] == profile)
            {
                wasServed = true;

                break;
            }
        }

        if(wasServed)
        {
            ChooseRandomProfile();
        }
        else
        {
            currentProfile = rand;
            SetProfile();
        }
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
        _currentClient.clientSatisfaction = profileSO[currentProfile].satisfaction;
        _currentClient.clientResolution = profileSO[currentProfile].resolution;
        _currentClient.clientCash = profileSO[currentProfile].cash;
        _currentClient.objectiveDescription = profileSO[currentProfile].encounters[0].objectives.objectiveDescription;
        if (profileSO[currentProfile].encounters?[0].objectives.character.Count > 0)
        {
            _currentClient.objectiveSprite = profileSO[currentProfile].encounters[0].objectives.character[0].fullSprite;
        }
        else
        {
            _currentClient.objectiveSprite = profileSO[currentProfile].encounters[0].objectives.item[0].sprite;
        }
        

        _dialogueManager.SetClientLines();
        _dialogueManager.SetPlayerResponses();

        _dialogueManager.maxDialogueGroup = profileSO[currentProfile].encounters[0].dialogueGroups.Count;
        _dialogueManager.currentDialogueGroup = 0;

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
        _dialogueBoxBackground.color = randomColor;
    }
}