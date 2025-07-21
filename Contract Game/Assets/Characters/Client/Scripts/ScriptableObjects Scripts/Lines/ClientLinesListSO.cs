using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientPersonalityListSO", menuName = "Client/LinesList")]
public class ClientLinesListSO : ScriptableObject
{
    public List<ClientGreetingLinesSO> linesList;
}
