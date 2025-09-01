using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossessions : MonoBehaviour
{
    public List<string> names = new List<string>();
    public int cash;
    public int souls;
    public List<ClientProfileSO> characters = new List<ClientProfileSO>();
    public List<ItemSO> items = new List<ItemSO>();
}
