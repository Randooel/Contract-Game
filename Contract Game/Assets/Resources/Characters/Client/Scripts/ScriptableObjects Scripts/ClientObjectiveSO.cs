using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClientObjective", menuName = "Client/Objective")]
public class ClientObjectiveSO : ScriptableObject
{
    public List<GameObject> objectives = new List<GameObject>();

    CurrentClient currentClient;

    void Start()
    {
        currentClient = FindObjectOfType<CurrentClient>();
    }

    public GameObject GetRandomObjective()
    {
        /*
        int rand = Random.Range(0, objectives.Count);
        return objectives[rand];
        */

        int rand1 = Random.Range(0, 3);

        for (int i = 0; i < rand1; i++)
        {
            int rand = Random.Range(0, objectives.Count);
            //currentClient.clientObjectives.Add(objectives[rand]);
        }

        if (objectives.Count > 0)
        {
            int finalRand = Random.Range(0, objectives.Count);
            return objectives[finalRand];
        }

        return null;
    }
}
