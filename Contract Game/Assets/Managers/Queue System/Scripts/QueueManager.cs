using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    [Header("Scripts References")]
    [SerializeField] private ClientManager _clientManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NextRandomClient()
    {
        _clientManager.GenerateClient();
    }

    public void NextRandomProfile()
    {
        _clientManager.ChooseRandomProfile();
    }
}
