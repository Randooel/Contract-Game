using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [Header("Client Reference")]
    CurrentClient currentClient;

    [Header("SO References")]
    public ClientNameSO clientNameSO;
    public ClientSpritesSO spritesSO;
    public ClientObjectiveSO objectSO;

    [Range(0,1)] public float resolution;
    public ClientPersonalitySO personalitySO;

    public int cash;
    // public GameObject[] possessions;

    void Start()
    {
        currentClient = FindObjectOfType<CurrentClient>();
    }

    void Update()
    {
        
    }

    void GenerateClient()
    {

    }
}