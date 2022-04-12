using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

namespace RoomCustomization
{
    public class PositionSaveController : MonoBehaviour
    {
        public List<GameObject> placedRooms;
        
        private readonly List<RoomInfo> _rooms = new();

        [DllImport("__Internal")]
        private static extern void PostJson(string path, string value, string objectName, string callback, string fallback);

        private void Update()
        {
            foreach (var room in placedRooms)
            {
                if (room == null)
                {
                    placedRooms.Remove(room);
                    break;
                }
            }
        }

        public void OnSave()
        {
            Debug.Log("Saving...");
            foreach (var room in placedRooms)
            {
                var roomInfo = new RoomInfo(room.transform.position, 
                    "./Assets/Sprites/NewSchoolView/Resources/" + room.name + ".png",
                    room.name);
                _rooms.Add(roomInfo);
            }
            SaveJson();
            Debug.Log("Written to db!");
            SceneManager.LoadScene("SchoolView");
        }

        public void SaveJson()
        {
            var output = JsonConvert.SerializeObject(_rooms);
            PostJson("SchoolCustomization", output, gameObject.name, "null", "null");
        }
    }
}