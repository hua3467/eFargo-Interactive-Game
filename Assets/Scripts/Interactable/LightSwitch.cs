using System;
using UnityEngine;

namespace Interactable
{
    public class LightSwitch: MonoBehaviour
    {
        public GameObject background;

        private RoomLighting _script;
        
        
        private void Start()
        {
            _script = background.GetComponent<RoomLighting>();
        }

        private void OnMouseDown()
        {
            _script.roomLightsOn = !_script.roomLightsOn;
        }
    }
}