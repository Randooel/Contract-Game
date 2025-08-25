using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyTextEffects.Editor.MyBoxCopy.Attributes;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
[System.Serializable]
public class ClientProfileSO : PossessionSO
{
    [Header("Visual")]
    public NameSO clientName;

    [Space(10)]
    public Sprite eyesSprite;
    public Sprite headSprite;
    public Sprite teethSprite;
    public Sprite outlineSprite;

    [Header("Moral")]
    [Range(-4, 3)] public float satisfaction;
    [Range(0, 1)] public float resolution;

    [Header("Property")]
    public int cash;
    public List<PossessionSO> possessions = new List<PossessionSO>();

    [Header("Dialogue")]
    public List<Encounters> encounters = new List<Encounters>();
}

[System.Serializable]
public class Encounters
{
    public string encounterTag;

    [Header("Objective")]
    public Objectives objectives;

    [Header("Dialogue")]
    public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();
}

[System.Serializable]
public class Objectives
{
    public string objectiveDescription;

    public PossessionSO objective;
}