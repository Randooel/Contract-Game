using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NegotiationLines", menuName = "Client/Negotiation Lines")]
public class ClientNegotiationLinesSO : ScriptableObject
{
    public List<string> lines = new List<string>();
}
