using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentClient : MonoBehaviour
{
    private Animator _animator;
    private DialogueManager _dialogueManager;

    [Header("Profile")]
    public string clientName;
    public TextMeshProUGUI clientTextName;
    public Sprite clientSprite;
    public SpriteRenderer clientSpriteRenderer;
    public List<GameObject> clientObjectives = new List<GameObject>();

    [Header("Personality")]
    [SerializeField][Range(0, 1)] public float clientResolution;
    public ClientGreetingLinesSO clientPersonality;

    [SerializeField]
    public enum State
    {
        Neutral,
        Happy,
        Angry
    }

    [Header("Negotiation Reference")]
    protected NegotiationManager negotiationManager;

    [Header("Physical Goods")]
    public int clientCash;
    public GameObject[] clientPossessions;

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
    }

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
            if(currentLine < sentences.Count)
            {
                Speak();
            }
            else
            {
                StopTalk();
            }
        }
        */
    }



    // DIALOGUE
    public void Speak()
    {
        _clientDialogueBox.SetActive(true);
        _clientDialogueText.text = lines[_dialogueManager.clientCurrentLine];
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
