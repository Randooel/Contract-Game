using EasyTextEffects.Editor.MyBoxCopy.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[System.Serializable]
public class PossessionList
{
    public string description;

    public List<string> names = new List<string>();
    public List<Visuals> visuals = new List<Visuals>();
    public int cash;

    [Space(20)]
    public bool useStatus;
    [ConditionalField(nameof(useStatus), false)]
    [Range(-4, 3)] public float satisfaction;
    [ConditionalField(nameof(useStatus), false)]

    [Space(10)]
    [Range(0, 1)] public float resolution;
    public List<ClientProfileSO> characters = new List<ClientProfileSO>();
    public List<ItemSO> item = new List<ItemSO>();
}

[System.Serializable]
public class Visuals
{
    public Sprite eye;
    public Sprite head;
    public Sprite teeth;
    public Sprite outline;

    [Space(10)]
    public Sprite fullSprite;
}
