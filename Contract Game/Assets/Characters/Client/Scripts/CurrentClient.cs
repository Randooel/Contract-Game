using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EasyTextEffects;
using JetBrains.Annotations;

public class CurrentClient : MonoBehaviour
{
    private Animator _animator;
    private DialogueManager _dialogueManager;

    [Header("Profile")]
    public string clientName;
    public TextMeshProUGUI clientTextName;
    public SpriteRenderer clientSpriteRenderer;

    [Header("Visual")]
    public GameObject[] reactions;

    [Header("Personality")]
    [Range(-4, 3)] public float clientSatisfaction;
    [SerializeField][Range(0, 1)] public float clientResolution;

    [SerializeField]
    public enum State
    {
        Neutral,
        Happy,
        Angry
    }

    [Header("Negotiation Reference")]
    protected NegotiationManager negotiationManager;

    [Header("Objectives")]
    public PossessionSO objective;
    public string objectiveDescription;
    public Sprite objectiveSprite;

    [Header("Possessions")]
    public int clientCash;

    public List<PossessionSO> clientPossessions = new List<PossessionSO>();

    [Header("Dialogue")]
    [SerializeField] protected GameObject _clientDialogueBox;
    [SerializeField] protected TextMeshProUGUI _clientDialogueText;
    // [SerializeField] private float typingSpeed = 0.05f;
    public int currentLine = 0;
    public List<string> lines = new List<string>();
    

    private Coroutine typingCoroutine;

    private void Start()
    {
        _clientDialogueBox.SetActive(false);

        // References check
        _animator = GetComponent<Animator>();

        _dialogueManager = FindObjectOfType<DialogueManager>();

        if (clientSpriteRenderer == null)
        {
            clientSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (clientTextName == null)
        {
            Debug.LogError("TMP component not assigned to clientTextName variable.");
        }

        if (negotiationManager == null)
        {
            negotiationManager = FindObjectOfType<NegotiationManager>();

            if (negotiationManager == null)
            {
                Debug.LogError("NegotiationManager not found!");
            }
        }

        foreach(var reaction in reactions)
        {
            reaction.SetActive(false);
        }
    }

    void Update()
    {
        
    }


    // DIALOGUE
    public void Speak()
    {
        var textEffect = _clientDialogueText.GetComponent<TextEffect>();

        _clientDialogueText.text = lines[_dialogueManager.currentLine];
        _clientDialogueBox.SetActive(true);

        textEffect.StopAllEffects();
        textEffect.Refresh();

        textEffect.StartManualEffect("Typewritter");
        textEffect.StartManualTagEffect("RainbowWave");

        _animator.SetTrigger("Speak");
    }

    public void StopTalk()
    {
        _clientDialogueBox.SetActive(false);
    }

    // ANIMATIONS
    public void PlayEntrance()
    {
        _animator.SetTrigger("Entrance");
    }

    public void PlayLeave()
    {
        _animator.SetTrigger("Leave");
    }
}
