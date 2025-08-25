using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTextEffects.Editor.MyBoxCopy.Attributes;

[CreateAssetMenu(fileName = "Item", menuName = "Possession/Item")]
public class ItemSO : PossessionSO
{
    [Header("Item Info")]
    public string description;

    [Space(5)]
    public bool containsSomething;

    [ConditionalField(nameof(containsSomething), false)]
    public PossessionSO content;
}