using System;
using UnityEngine;

namespace Interactable
{
    public class LightSwitch: MonoBehaviour
    {
        public RoomLighting background;

        private void OnMouseDown()
        {
            background.roomLightsOn = !background.roomLightsOn;
            var renderer = GetComponent<SpriteRenderer>();
            renderer.flipY = !renderer.flipY;
        }
    }
}