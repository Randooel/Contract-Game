using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
[System.Serializable]
public class ClientProfileSO : ScriptableObject
{
    [Header("Visual")]
    public string clientName;
    [Space(10)]
    public Sprite eyesSprite;
    public Sprite headSprite;
    public Sprite teethSprite;
    public Sprite outlineSprite;

    [Space(5)]
    public Sprite fullSprite;

    [Header("Moral")]
    [Range(-10, 10)] public float satisfaction;
    [Range(0, 1)] public float resolution;

    [Header("Property")]
    public int cash;
    public List<ScriptableObject> possessions = new List<ScriptableObject>();

    [Header("Dialogue")]
    public List<Encounters> encounters = new List<Encounters>();
}

[System.Serializable]
public class Encounters
{
    public string encounterTag;
    public Objectives objectives;
    public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();
}

[System.Serializable]
public class Objectives
{
    public string objectiveDescription;
    public List<ClientProfileSO> character = new List<ClientProfileSO>();
    public List<ItemSO> item = new List<ItemSO>();
}