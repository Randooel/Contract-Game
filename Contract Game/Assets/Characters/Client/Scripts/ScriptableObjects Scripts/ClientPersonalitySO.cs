using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Personality", menuName = "Client/Personality")]
public class ClientPersonalitySO : ScriptableObject
{
    public string personalityTrait;

    [System.Serializable]
    public class PersonalityLines
    {
        public string linesType;
        public List<string> lines = new List<string>();
    }

    public List<PersonalityLines> LinesGroups = new List<PersonalityLines>();
}
