using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientSO", menuName = "Client/Profile")]
[System.Serializable]
public class ClientProfileSO : ScriptableObject
{
    public string clientName;
    public Sprite eyesSprite;
    public Sprite headSprite;
    public Sprite teethSprite;
    public Sprite outlineSprite;
    [Range(0, 1)] public float resolution;
    public GameObject[] objective;
    public int cash;
    public GameObject[] possession;

    [System.Serializable]
    public class DialogueGroup
    {
        [TextArea(1, 3)]
        public List<string> clientLines;

        [TextArea(1, 2)]
        public List<string> playerResponses;
    }

    [Space(10)]
    public List<DialogueGroup> dialogueGroups = new List<DialogueGroup>();

}
