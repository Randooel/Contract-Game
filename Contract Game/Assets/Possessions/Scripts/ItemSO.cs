using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using EasyTextEffects.Editor.MyBoxCopy.Attributes;

[CreateAssetMenu(fileName = "Item", menuName = "Possession/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string description;
    public Sprite sprite;

    public bool inspectorTest;

    /*
    [ConditionalField(nameof(inspectorTest), false)]
    [Range(-1, 1)] public int vitaminC;
    */

    [Header("If it contains a character")]
    public ClientProfileSO clientProfile;
}
