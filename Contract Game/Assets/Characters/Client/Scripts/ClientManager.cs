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
    public ClientObjectiveSO objectivesSO;
    public ClientPersonalitySO personalitySO;

    public List<ClientSO> profileSO = new List<ClientSO>();
    public int currentProfile;

    void Start()
    {
        currentClient = FindObjectOfType<CurrentClient>();

        ChooseRandomProfile();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ChooseRandomProfile();
        }
    }

    void ChooseRandomProfile()
    {
        int rand = Random.Range(0, profileSO.Count);
        currentProfile = rand;

        SetProfile();
    }

    void SetProfile()
    {
        currentClient.clientName = profileSO[currentProfile].clientName;
        currentClient.clientTextName.text = profileSO[currentProfile].clientName;
        currentClient.clientSpriteRenderer.sprite = profileSO[currentProfile].sprite;
        RandomizeColor();
        //currentClient.clientObjectives = profileSO[currentProfile].objective;
        currentClient.clientResolution = profileSO[currentProfile].resolution;
        currentClient.clientCash = profileSO[currentProfile].cash;
    }
    void GenerateClient()
    {
        currentClient.clientName = clientNameSO.name;
        currentClient.clientSprite = spritesSO.GetRandomSprite();
        objectivesSO.GetRandomObjective();
        RandomizeResolution();
        RandomizeCash();
        RandomizeColor();
    }

    void RandomizeSprite()
    {
        int rand = Random.Range(0, spritesSO.sprites.Count);
        currentClient.clientSprite = spritesSO.sprites[rand];
    }

    void RandomizeCash()
    {
        int rand = Random.Range(0, 999);
        currentClient.clientCash = rand;
    }

    void RandomizeResolution()
    {
        float rand = Random.Range(0, 1);
        currentClient.clientResolution = rand;
    }

    void RandomizeColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        currentClient.clientSpriteRenderer.color = randomColor;
    }
}