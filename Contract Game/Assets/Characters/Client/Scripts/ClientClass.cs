using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClientClass : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] protected string clientName;
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected string objective;

    [Header("Personality")]
    [SerializeField][Range(0, 1)] protected float resolution;
    [SerializeField] protected ScriptableObject personality;

    [SerializeField] protected enum State
    {
        Neutral,
        Happy,
        Angry
    }

    [Header("Physical Goods")]
    [SerializeField] protected int cash;
    [SerializeField] protected GameObject[] possessions;

    [Header("Random Values")]
    protected int[] randomizedValues;

    protected virtual void RandomizeValues()
    {
        randomizedValues = new int[7];

        for (int i = 0; i < 7; i++)
        {
            int rand = Random.Range(0, 10);
            randomizedValues[i] = rand;
        }
    }
}
