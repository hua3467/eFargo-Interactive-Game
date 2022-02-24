using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class RoomMapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, string> mappings = LoadRoomMapping();
        var objects = GameObject.FindGameObjectsWithTag("Rooms");
        foreach (var obj in objects)
        {
            var hoverScript = obj.GetComponent<ClassroomHover>();
            var objName = obj.name;
            hoverScript.sceneName = mappings[objName];
        }
    }

    private Dictionary<string, string> LoadRoomMapping()
    {
        using (var r = new StreamReader("./Assets/Scripts/RoomMapping/RoomMapping.json"))
        {
            string json = r.ReadToEnd();
            Dictionary<string, string> mappings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return mappings;
        }
    }
}
