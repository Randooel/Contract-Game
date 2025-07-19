using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ClientClass : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] public string clientName;
    [SerializeField] public TextMeshProUGUI clientTextName;
    [SerializeField] public Sprite clientSprite;
    [SerializeField] public SpriteRenderer clientSpriteRenderer;
    [SerializeField] public List<GameObject> clientObjectives = new List<GameObject>();

    [Header("Personality")]
    [SerializeField][Range(0, 1)] public float clientResolution;
    [SerializeField] public ScriptableObject clientPersonality;

    [SerializeField]
    public enum State
    {
        Neutral,
        Happy,
        Angry
    }

    [Header("Physical Goods")]
    [SerializeField] public int clientCash;
    [SerializeField] public GameObject[] clientPossessions;

    private void Start()
    {
        if(clientSpriteRenderer == null)
        {
            clientSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        if(clientTextName == null)
        {
            Debug.LogError("TMP component not assigned to clientTextName variable.");
        }
    }
}
