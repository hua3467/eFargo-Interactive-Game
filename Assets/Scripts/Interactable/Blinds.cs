using System;
using UnityEngine;

public class Blinds : MonoBehaviour
{
    public RoomLighting background;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnMouseDown()
    {
        background.daylightOn = !background.daylightOn;
    }
}
