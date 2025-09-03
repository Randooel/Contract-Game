using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EasyTextEffects;
using JetBrains.Annotations;

public class CurrentClient : MonoBehaviour
{
    private DialogueManager _dialogueManager;
    private ClientManager _clientManager;

    private Animator _animator;

    [Header("Profile")]
    public string clientName;
    public TextMeshProUGUI textName;
    public SpriteRenderer spriteRenderer;

    [Header("Visual")]
    public GameObject[] reactions;

    [Header("Personality")]
    [Range(-4, 3)] public float satisfaction;
    [SerializeField][Range(0, 1)] public float resolution;

    [SerializeField]
    public enum State
    {
        Neutral,
        Happy,
        Angry
    }

    [Header("Negotiation Reference")]
    protected NegotiationManager negotiationManager;

    public PossessionList objectives;

    [Space(10)]
    public PossessionList possessions;

    [Header("Dialogue")]
    [SerializeField] protected GameObject _dialogueBox;
    [SerializeField] protected TextMeshProUGUI _dialogueText;
    // [SerializeField] private float typingSpeed = 0.05f;
    public int currentLine = 0;
    public List<string> lines = new List<string>();
    

    private Coroutine typingCoroutine;

    private void Start()
    {
        _dialogueBox.SetActive(false);

        // References check
        _animator = GetComponent<Animator>();

        _dialogueManager = FindObjectOfType<DialogueManager>();

        _clientManager = FindObjectOfType<ClientManager>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (textName == null)
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

    public void UpdatePossessions()
    {
        
    }

    public void ResetPossessions()
    {

    }


    // DIALOGUE
    public void Speak()
    {
        var textEffect = _dialogueText.GetComponent<TextEffect>();

        _dialogueText.text = lines[_dialogueManager.currentLine];
        _dialogueBox.SetActive(true);

        textEffect.StopAllEffects();
        textEffect.Refresh();

        textEffect.StartManualEffect("Typewritter");
        textEffect.StartManualTagEffect("RainbowWave");

        _animator.SetTrigger("Speak");
    }

    public void StopTalk()
    {
        _dialogueBox.SetActive(false);
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
