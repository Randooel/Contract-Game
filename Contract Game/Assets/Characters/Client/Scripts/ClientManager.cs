using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [Header("Client Reference")]
    CurrentClient currentClient;
    [SerializeField] SpriteRenderer clientEyes;
    [SerializeField] SpriteRenderer clientHead;
    [SerializeField] SpriteRenderer clientTeeth;
    [SerializeField] SpriteRenderer clientOutline;

    [Header("SO References")]
    public ClientNameSO clientNameSO;
    public ClientSpritesListSO spritesListSO;
    public ClientObjectiveSO objectivesSO;
    public ClientPersonalitySO personalitySO;

    public List<ClientSO> profileSO = new List<ClientSO>();
    public int currentProfile;

    void Start()
    {
        currentClient = FindObjectOfType<CurrentClient>();

        //ChooseRandomProfile();
        GenerateClient();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            GenerateClient();
            //ChooseRandomProfile();
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
        //currentClient.clientSprite = spritesSO.GetRandomSprite();
        RandomizeSprite();
        //objectivesSO.GetRandomObjective();
        RandomizeResolution();
        RandomizeCash();
        RandomizeColor();
    }

    void RandomizeSprite()
    {
        /*
        int rand = Random.Range(0, spritesListSO.clientSpritesListSOs.Count);
        currentClient.clientSprite = spritesListSO.clientSpritesListSOs.ClientSpriteSO.sprites[rand];
        */

        int rand = Random.Range(0, spritesListSO.clientSpritesListSO.Count);
        var selectedSO = spritesListSO.clientSpritesListSO[rand];

        clientEyes.sprite = selectedSO.clientEyes;
        clientHead.sprite = selectedSO.clientHead;
        clientTeeth.sprite = selectedSO.clientTeeth;
        clientOutline.sprite = selectedSO.clientOutline;
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
        clientHead.color = randomColor;
    }
}