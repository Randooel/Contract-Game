using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
public class ClientSO : ScriptableObject
{
    public string clientName;
    public Sprite eyesSprite;
    public Sprite headSprite;
    public Sprite teethSprite;
    public Sprite outlineSprite;
    [Range(0, 1)] public float resolution;
    public GameObject[] objective;
    public int cash;
    public GameObject[] possession;

    // greetings
    public List<string> greetingLines = new List<string>();
    public List<string> playerGreetingResponses = new List<string>();

    [Space(10)]
    public List<string> negotiationLines = new List<string>();
    public List<string> playerNegotiationResponses = new List<string>();

    public List<string> positiveConclusionLines = new List<string>();
    public List<string> neutralConclusionLines = new List<string>();
    public List<string> negativeConclusionLines = new List<string>();

}
