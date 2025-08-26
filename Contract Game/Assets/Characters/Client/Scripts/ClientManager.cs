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

    /*
    [Header("SO References")]
    public ClientNameSO clientNameSO;
    public ClientSpritesListSO spritesListSO;
    public ClientObjectiveSO objectivesSO;
    
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
        if(_queueManager.servedClients.Count < profileSO.Count)
        {
            if (canCallNextClient == true)
            {
                canCallNextClient = false;

                _currentClient.lines.Clear();
                _currentClient.clientPossessions.Clear();
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
        else
        {
            _queueManager.HandleNextDay();
        }
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
        int rand = Random.Range(0, profileSO.Count);

        _queueManager.CheckServed(rand);        
    }

    public void SetProfile()
    {
        var profile = profileSO[currentProfile];
        var currentEncounter = _dialogueManager.currentEncounter;

        _currentClient.clientName = profile.clientName.possessionName;
        _currentClient.clientTextName.text = profile.clientName.possessionName;
        _clientEyes.sprite = profile.eyesSprite;
        _clientHead.sprite = profile.headSprite;
        _clientTeeth.sprite = profile.teethSprite;
        _clientOutline.sprite = profile.outlineSprite;
        RandomizeColor();
        //currentClient.clientObjectives = profileSO[currentProfile].objective;
        _currentClient.clientSatisfaction = profile.satisfaction;
        _currentClient.clientResolution = profile.resolution;
        _currentClient.clientCash = profile.cash;

        _currentClient.objective = profile.encounters[currentEncounter].objectives.objective;
        _currentClient.objectiveDescription = profile.encounters[currentEncounter].objectives.objectiveDescription;
        _currentClient.objectiveSprite = profile.encounters[currentEncounter].objectives.objective.possessionSprite.sprite;

        if(profile.possessions != null)
        {
            foreach(var possession in profile.possessions)
            {
                _currentClient.clientPossessions.Add(possession);
            }
        }

        // Possessions
        _currentClient.clientPossessions.Add(profile.clientName);
        _currentClient.clientPossessions.Add(profile.possessionSprite);

        _dialogueManager.SetClientLines();
        _dialogueManager.SetPlayerResponses();

        _dialogueManager.maxDialogueGroup = profile.encounters[0].dialogueGroups.Count;
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