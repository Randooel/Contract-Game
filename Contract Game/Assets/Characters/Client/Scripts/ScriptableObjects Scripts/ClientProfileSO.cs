using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
[System.Serializable]
public class ClientProfileSO : ScriptableObject
{
    public string clientName;
    public Sprite eyesSprite;
    public Sprite headSprite;
    public Sprite teethSprite;
    public Sprite outlineSprite;
    public float satisfaction;
    [Range(0, 1)] public float resolution;
    public GameObject[] objective;
    public int cash;
    public GameObject[] possession;

    [System.Serializable]
    public class Encounters
    {
        public string encounterTag;
        public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();
    }

    public List<Encounters> encounters = new List<Encounters>();
}