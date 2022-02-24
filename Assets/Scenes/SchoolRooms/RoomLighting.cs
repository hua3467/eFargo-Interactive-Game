using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLighting : MonoBehaviour
{
    public bool roomLightsOn = true;
    public bool daylightOn = true;
    public Sprite lightsOn;
    public Sprite lightsOff;
    public Sprite lightsOffDaylight;
    public Sprite lightsOnDaylight;

    private SpriteRenderer _background;
    
    // Start is called before the first frame update
    void Start()
    {
        _background = GetComponent<SpriteRenderer>();
        SetLighting();
    }

    // Update is called once per frame
    void Update()
    {
        SetLighting();
    }

    private void SetLighting()
    {
        if (roomLightsOn)
        {
            if (daylightOn)
            {
                _background.sprite = lightsOnDaylight;
            }
            else
            {
                _background.sprite = lightsOn;
            }
        }
        else
        {
            if (daylightOn)
            {
                _background.sprite = lightsOffDaylight;
            }
            else
            {
                _background.sprite = lightsOff;
            }
        }
    }
}
