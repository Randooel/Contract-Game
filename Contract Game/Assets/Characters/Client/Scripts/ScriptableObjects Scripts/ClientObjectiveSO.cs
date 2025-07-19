using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientObjective", menuName = "Client/Objective")]
public class ClientObjectiveSO : ScriptableObject
{
    public List<GameObject> objectives = new List<GameObject>();
}
