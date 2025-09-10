using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppraisePrice : MonoBehaviour
{
    [Header("Other Scripts")]
    private ClientManager _clientManager;
    private CurrentClient _currentClient;
    private ContractManager _contractManager;

    public ClientProfileSO profileSO;

    [Header("Evaluation Variables")]
    [Range(-4, 3)] public float satisfaction;
    [Range(0, 1)] public float resolution;
    public PossessionsValues possessionValues;

    [Header("Terms")]
    public List<GameObject> price, request = new List<GameObject>();

    void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        _currentClient = FindObjectOfType<CurrentClient>();
        _contractManager = FindObjectOfType<ContractManager>();
    }
    void Update()
    {
        
    }

    public void SetTerms(bool setPrice, bool setRequest)
    {
        if(setPrice)
        {
            //price = _contractManager._priceField[0];
        }
        if(setRequest)
        {
            //request = _contractManager._requestField[0];
        }
    }

    public void SetStatus()
    {
        satisfaction = _currentClient.satisfaction;
        resolution = _currentClient.resolution;
    }

    public void SetPossessionsValues()
    {
        possessionValues = profileSO.values;
    }
}