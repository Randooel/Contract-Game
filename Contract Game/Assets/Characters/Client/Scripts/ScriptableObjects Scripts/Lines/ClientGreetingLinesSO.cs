using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GreetingLines", menuName = "Client/Greeting Lines")]
public class ClientGreetingLinesSO : ScriptableObject
{
    public List<string> lines = new List<string>();
}
