using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTextEffects.Editor.MyBoxCopy.Attributes;

[CreateAssetMenu(fileName = "Item", menuName = "Possession/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string description;
    public Sprite sprite;

    [Space(5)]
    public bool containsSomething;

    //[ConditionalField(nameof(containsSomething), false)]
    public object content;
}