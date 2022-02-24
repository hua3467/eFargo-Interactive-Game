using System;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer: MonoBehaviour, IClickable
{
    public GameObject popUp;

    private void Start()
    {
        popUp.SetActive(false);
    }

    public void Clicked()
    {
        popUp.SetActive(true);
    }

    public void UnClicked()
    {
        popUp.SetActive(false);
    }
}