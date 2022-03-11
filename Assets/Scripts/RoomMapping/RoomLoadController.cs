using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using RoomCustomization;
using Utilities;

public class RoomLoadController : MonoBehaviour
{
    public List<GameObject> preloadedRooms;
    
    void Start()
    {
        var roomList = LoadRoomPositions();
        foreach (var room in roomList)
        {
            var query = from GameObject obj in preloadedRooms where obj.name == room.Name select obj;
            var roomTransform = new Vector3(room.Transform[0], room.Transform[1], room.Transform[2]);
            var newRoom = Instantiate(query.First(), roomTransform, Quaternion.identity);
            
            // set scaling of object
            // would rather not make this a magic number, but oh well
            newRoom.transform.localScale = new Vector3(.45f, .45f, .45f);
            newRoom.name = room.Name;
        }
    }

    private List<RoomInfo> LoadRoomPositions()
    {
        using (var r = new StreamReader("./Assets/Scripts/RoomMapping/RoomSaving.json"))
        {
            string json = r.ReadToEnd();
            var rooms = JsonConvert.DeserializeObject<List<RoomInfo>>(json);
            return rooms;
        }
    }
}
