using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PossessionsValues
{
    public enum Values
    {
        High,
        Medium,
        Low
    }

    public Values name, visuals, cash, satisfaction, resolution, items, characters;
}

