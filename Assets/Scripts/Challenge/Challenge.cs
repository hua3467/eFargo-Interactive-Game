using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Challenge
{
    public Sprite Monster;

    [NonSerialized]
    public bool Completed = false;
    
    [JsonProperty("Dialog")]
    public readonly Queue<string> Dialog;
    
    [JsonProperty("TagType")]
    public string TagType { get; set; }
    
    [JsonProperty("MonsterName")]
    public string MonsterName { get; set; }
    
    [JsonProperty("CarbonCoins")]
    public int CarbonCoins { get; set; }
    
    [JsonProperty("SceneName")]
    public string SceneName { get; set; }

    [JsonConstructor]
    public Challenge(List<string> dialog, string tagType, string monsterName, int carbonCoins, string sceneName)
    {
        Dialog = new Queue<string>(dialog);
        TagType = tagType;
        MonsterName = monsterName;
        CarbonCoins = carbonCoins;
        SceneName = sceneName;
    }
}