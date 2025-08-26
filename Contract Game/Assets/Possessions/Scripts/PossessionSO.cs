using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTextEffects.Editor.MyBoxCopy.Attributes;

public class PossessionSO : ScriptableObject
{
    public bool hidePossessionInfo;

    [Header("Possession Info")]
    [ConditionalField(nameof(hidePossessionInfo), true)]
    public string possessionName;

    [ConditionalField(nameof(hidePossessionInfo), true)]
    public SpriteSO possessionSprite;

    [ConditionalField(nameof(hidePossessionInfo), true)]
    [Space(10)]
    public PossessionType possessionType;

    public enum PossessionType
    { 
        Name,
        Sprite,
        Satisfaction,
        Resolution,
        Cash,
        Character,
        Item
    }
}