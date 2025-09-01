using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDebt : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        ResetDebt();

        if(slider == null)
        {
            slider = FindObjectOfType<Slider>();
        }
    }

    void Update()
    {
        
    }

    public void ResetDebt()
    {
        slider.value = 0;
    }

    public void UpdateDebt(int value)
    {
        slider.value += value;
    }

    public void UpdateMaxDebt(int maxValue)
    {
        slider.maxValue = maxValue;
    }
}
