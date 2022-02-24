using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RoomCustomization
{
    public class PositionSaveController : MonoBehaviour
    {
        public List<GameObject> placedRooms;

        // dictionary for json structure of "name: [transform, SpritePath]"
        private List<RoomInfo> _rooms = new List<RoomInfo>();

        public void OnSave()
        {
            Debug.Log("Saved");
            foreach (var room in placedRooms)
            {
                var roomInfo = new RoomInfo(room.transform.position, 
                    "./Assets/Sprites/NewSchoolView/Resources/" + room.name + ".png",
                    room.name);
                _rooms.Add(roomInfo);
            }

            var output = JsonConvert.SerializeObject(_rooms);
            using (var w = new StreamWriter("./Assets/Scripts/RoomMapping/RoomSaving.json"))
            {
                w.Write(output);
            }
            
        }
    }
}