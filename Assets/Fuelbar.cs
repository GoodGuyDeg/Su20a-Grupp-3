using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fuelbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFuel(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;
    }

    public void SetHealth(float fuel)
    {
        slider.value = fuel;
    }
}
