using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSpriteListSO", menuName = "Client/SpriteList")]
public class ClientSpritesListSO : ScriptableObject
{
    public List<ClientSpritesSO> clientSpritesListSO = new List<ClientSpritesSO>();

    /*
    public ClientSpritesSO GetRandomSprite()
    {
        int rand = Random.Range(0, clientSpritesListSOs.Count);
        return clientSpritesListSOs[rand];
    }
    */
}
