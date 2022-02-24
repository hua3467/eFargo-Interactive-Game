using System;
using Newtonsoft.Json;
using UnityEngine;

namespace RoomCustomization
{
    [Serializable]
    public class RoomInfo
    {
        [JsonProperty("Transform")]
        public float[] Transform { get; }
        
        [JsonProperty("SpriteLocation")]
        public string SpriteLocation { get; }

        [JsonProperty("name")]
        public string Name { get; }
        
        public RoomInfo(Vector3 transform, string spriteLocation, string name)
        {
            Transform = new[] { transform.x, transform.y, transform.z };
            SpriteLocation = spriteLocation;
            Name = name;
        }

        [JsonConstructor]
        public RoomInfo(float[] transform, string spriteLocation, string name)
        {
            Transform = transform;
            SpriteLocation = spriteLocation;
            Name = name;
        }
    }
}