using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
public class ClientSO : ScriptableObject
{
    public string clientName;
    public Sprite sprite;
    [Range(0, 1)] public float resolution;
    public GameObject[] objective;
    public int cash;
    public GameObject[] possession;
}
