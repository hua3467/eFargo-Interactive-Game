using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using RoomCustomization;
using Utilities;

public class RoomLoadController : MonoBehaviour
{
    public List<GameObject> loadedRooms;
    public GameObject roomPrefab;
    public GameObject hallwayPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var roomList = LoadRoomPositions();
        foreach (var room in roomList)
        {
            var roomTransform = new Vector3(room.Transform[0], room.Transform[1], room.Transform[2]);
            var newRoom = Instantiate(hallwayPrefab, roomTransform, Quaternion.identity);
            newRoom.GetComponent<SpriteRenderer>().sprite = ImgToSprite.LoadNewSprite(room.SpriteLocation);
            // refresh polygon collider
            Destroy(newRoom.GetComponent<PolygonCollider2D>());
            newRoom.AddComponent<PolygonCollider2D>();
            // fix scaling issue from refreshing collider
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
