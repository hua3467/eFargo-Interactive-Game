using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace RoomCustomization
{
    public class PositionSaveController : MonoBehaviour
    {
        public List<GameObject> placedRooms;
        
        private readonly List<RoomInfo> _rooms = new();

        [DllImport("__Internal")]
        private static extern void PostJson(string path, string value, string objectName, string callback, string fallback);

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
        }

        public void SaveJson()
        {
            var output = JsonConvert.SerializeObject(_rooms);
            PostJson("SchoolCustomization", output, "SchoolCustomization", "null", "null");
        }
    }
}