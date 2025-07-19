using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSprite", menuName = "Client/Sprite")]
public class ClientSpritesSO : ScriptableObject
{
    public Sprite clientEyes;
    public Sprite clientHead;
    public Sprite clientTeeth;
    public Sprite clientOutline;

    /*
    public List<Sprite> sprites = new List<Sprite>();
    
    public Sprite GetRandomSprite()
    {
        int rand = Random.Range(0, sprites.Count);
        return sprites[rand];
    }
    */
}
