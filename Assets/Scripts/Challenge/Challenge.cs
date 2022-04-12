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

    [JsonProperty("ChallengeName")] 
    public string ChallengeName { get; set; }
    
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
    public Challenge(string challengeName, string[] dialog, string tagType, string monsterName, int carbonCoins, string sceneName)
    {
        ChallengeName = challengeName;
        Dialog = new Queue<string>(dialog);
        TagType = tagType;
        MonsterName = monsterName;
        CarbonCoins = carbonCoins;
        SceneName = sceneName;
    }
}