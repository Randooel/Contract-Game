using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PossessionList : MonoBehaviour
{
    public string name;
    public string description;
    public Sprite sprite;
    public int cash;
    public ItemSO item;
    public ClientProfileSO character;
}
