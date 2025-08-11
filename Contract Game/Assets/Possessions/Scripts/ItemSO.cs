using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Possession/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Item Info")]
    public string itemName;
    public string description;
    public Sprite sprite;

    [Header("If it contains a character")]
    public ClientProfileSO clientProfile;
}
