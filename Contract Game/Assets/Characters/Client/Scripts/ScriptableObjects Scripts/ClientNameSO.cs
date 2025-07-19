using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientName", menuName = "Client/Name")]

public class ClientNameSO : ScriptableObject
{
    public List<string> clientNames = new List<string>();

    public string GetRandomName()
    {
        int rand = Random.Range(0, clientNames.Count);
        return clientNames[rand];
    }
}
