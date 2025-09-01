using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResponses : MonoBehaviour
{
    private CurrentClient currentClient;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer _playerRenderer;
    public Sprite[] playerExpressions;
    public SpriteRenderer[] reactions;

    [Header("Responses")]
    public GameObject[] responsesObjects;
    public TextMeshProUGUI[] responsesText;
    
    public List<string> responsesLines = new List<string>();

    public bool isReponseActive;

    private void Start()
    {
        currentClient = FindObjectOfType<CurrentClient>();

        HideResponses();

        foreach(var reaction in reactions)
        {
            reaction.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        
    }

    public void ShowResponses(/*string response1, string response2, string response3*/)
    {
        UpdateResponses();

        isReponseActive = true;

        for(int i = 0; i < responsesObjects.Length; i++)
        {
            responsesObjects[i].gameObject.SetActive(true);
        }
    }
    
    public void HideResponses()
    {
        ClearResponses();

        isReponseActive = false;

        for (int i = 0; i < responsesObjects.Length; i++)
        {
            responsesObjects[i].gameObject.SetActive(false);
        }
    }

    private void UpdateResponses()
    {
        for(int i = 0; i < responsesLines.Count; i++)
        {
            responsesText[i].text = responsesLines[i];
        }
    }

    private void ClearResponses()
    {
        responsesLines.Clear();
    }

    // REACTIONS
    public void Reaction(int expressionIndex, int reactionIndex)
    {
        _playerRenderer.sprite = playerExpressions[expressionIndex];
        reactions[reactionIndex].gameObject.SetActive(true);

        StartCoroutine(WaitToIdle());
    }

    private IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(1f);

        _playerRenderer.sprite = playerExpressions[0];

        foreach (var reaction in reactions)
        {
            reaction.gameObject.SetActive(false);
        }
    }
}
