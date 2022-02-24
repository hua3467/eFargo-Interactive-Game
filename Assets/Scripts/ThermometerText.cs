using System;
using UnityEngine;
using UnityEngine.UI;

public class ThermometerText: MonoBehaviour
{
    public Slider sliderUI;
    private Text _thermostatText;

    private void Start()
    {
        _thermostatText = GetComponent<Text>();
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        _thermostatText.text = sliderUI.value + " F";
    }
}