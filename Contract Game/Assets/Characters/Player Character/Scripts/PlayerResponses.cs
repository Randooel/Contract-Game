using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResponses : MonoBehaviour
{
    public GameObject[] responsesObjects;
    public TextMeshProUGUI[] responsesText;
    public List<string> responsesLines = new List<string>();

    private void Start()
    {
        HideResponses();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            HideResponses();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ShowResponses();
        }
    }

    public void ShowResponses(/*string response1, string response2, string response3*/)
    {
        UpdateResponses();

        for(int i = 0; i < responsesObjects.Length; i++)
        {
            responsesObjects[i].gameObject.SetActive(true);
        }
    }
    
    public void HideResponses()
    {
        ClearResponses();

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
}
