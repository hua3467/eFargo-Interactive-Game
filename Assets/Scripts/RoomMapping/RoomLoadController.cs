using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using Newtonsoft.Json;
using RoomCustomization;

public class RoomLoadController : MonoBehaviour
{
    public List<GameObject> preloadedRooms;

    private List<RoomInfo> _roomList;

    [DllImport("__Internal")]
    private static extern string GetJson(string path, string objectName, string callback, string fallback);
    
    void Start()
    {
        GetJson("SchoolCustomization", gameObject.name, "LoadRoomPositions", "LoadRoomPositions");
    }

    private void LoadRooms()
    {
        foreach (var room in _roomList)
        {
            var query = from GameObject obj in preloadedRooms where obj.name == room.Name select obj;
            var roomTransform = new Vector3(room.Transform[0], room.Transform[1], room.Transform[2]);
            var newRoom = Instantiate(query.First(), roomTransform, Quaternion.identity);
            
            // set scaling of object
            // would rather not make this a magic number, but oh well
            newRoom.transform.localScale = new Vector3(.45f, .45f, .45f);
            newRoom.name = room.Name;
            
            // set mapping for click to room scene
            var hoverScript = newRoom.GetComponent<ClassroomHover>();
            hoverScript.sceneName = newRoom.name;
        }
    }

    private void LoadRoomPositions(string data)
    {
        Debug.Log("Reading from db...");
        _roomList = JsonConvert.DeserializeObject<List<RoomInfo>>(data);
        LoadRooms();
    }
}
