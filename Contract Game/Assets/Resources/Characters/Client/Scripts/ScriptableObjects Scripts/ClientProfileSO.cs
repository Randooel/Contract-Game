using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyTextEffects.Editor.MyBoxCopy.Attributes;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
[System.Serializable]
public class ClientProfileSO : ScriptableObject
{
    [Header("Visual")]
    public string profileName;

    [Space(10)]
    public Sprite eyesSprite;
    public Sprite headSprite;
    public Sprite teethSprite;
    public Sprite outlineSprite;

    [Space(10)]
    public Sprite fullSprite;

    [Header("Moral")]
    [Range(-4, 3)] public float satisfaction;
    [Range(0, 1)] public float resolution;

    [Header("Property")]
    public PossessionList profilePossessions;

    [Header("Dialogue")]
    public List<Encounters> encounters = new List<Encounters>();
}

// [x]
[System.Serializable]
public class Encounters
{
    public string encounterTag;

    [Header("Objective")]
    public PossessionList objectives;

    [Header("Dialogue")]
    public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();
}