using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSprite", menuName = "Client/Sprite")]
public class ClientSpritesSO : ScriptableObject
{
    public List<Sprite> sprites = new List<Sprite>();
}
