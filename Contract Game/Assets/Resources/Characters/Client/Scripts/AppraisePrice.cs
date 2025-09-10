using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppraisePrice : MonoBehaviour
{
    [Header("Other Scripts")]
    private ClientManager _clientManager;
    private CurrentClient _currentClient;
    private ContractManager _contractManager;
    private DialogueManager _dialogueManager;

    public ClientProfileSO profileSO;

    [Header("Evaluation Variables")]
    [Range(-4, 3)] public float satisfaction;
    [Range(0, 1)] public float resolution;
    public PossessionsValues possessionValues;

    [Header("Terms")]
    public List<GameObject> price = new List<GameObject>();
    // There is no need to a REQUEST list, since it will be calculated by the resolution

    void Start()
    {
        _clientManager = FindObjectOfType<ClientManager>();
        _currentClient = FindObjectOfType<CurrentClient>();
        _contractManager = FindObjectOfType<ContractManager>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void SetPrice()
    {
        Debug.Log("SetPrice");

        price.Add(_contractManager.priceField[0]);

        CheckPrice();
    }

    // 1 = Low, 2 = Medium, 3 = High
    public void CheckPrice()
    {
        int value = 0;

        if (price[0].name.Contains("Name"))
        {
            value = GetValueFromEnum(possessionValues.name);
        }
        else if (price[0].name.Contains("Visual"))
        {
            value = GetValueFromEnum(possessionValues.visuals);
        }
        else if (price[0].name.Contains("Cash"))
        {
            value = GetValueFromEnum(possessionValues.cash);
        }
        else if (price[0].name.Contains("Satisfaction"))
        {
            value = GetValueFromEnum(possessionValues.satisfaction);
        }
        else if (price[0].name.Contains("Resolution"))
        {
            value = GetValueFromEnum(possessionValues.resolution);
        }
        else if (price[0].name.Contains("Item"))
        {
            value = GetValueFromEnum(possessionValues.items);
        }
        else if (price[0].name.Contains("Character"))
        {
            value = GetValueFromEnum(possessionValues.characters);
        }

        EvaluatePrice(value);
    }

    private int GetValueFromEnum(PossessionsValues.Values value)
    {
        switch (value)
        {
            case PossessionsValues.Values.High: return 3;
            case PossessionsValues.Values.Medium: return 2;
            case PossessionsValues.Values.Low: return 1;
            default: return 0;
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

    public void EvaluatePrice(int priceValue)
    {
        SetStatus();

        var finalValue = satisfaction + resolution + 1;

        Debug.Log(satisfaction);

        if(satisfaction <= -4)
        {
            // Critical Refuse logic
            _dialogueManager.PlayDialogue("CRITICAL REFUSE");
        }
        else if (resolution <= 1 || satisfaction <= 3)
        {
            Debug.Log("CRITICAL DEAL");
            // Critical Deal logic
            _dialogueManager.PlayDialogue("CRITICAL DEAL");
        }
        else if (finalValue > priceValue)
        {
            // Deal logic
            _dialogueManager.PlayDialogue("DEAL");
        }
        else
        {
            // Refuse logic
            _dialogueManager.PlayDialogue("REFUSE");
        }
    }

    public void ClearPrice()
    {
        price.Clear();

        satisfaction = 0;
        resolution = 0;
    }
}