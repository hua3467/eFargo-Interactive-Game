using System.Collections.Generic;
using UnityEngine;

public class Challenge
{
    public Sprite Monster;
    public readonly Queue<string> Dialog;
    public string TagType { get; set; }
    public string MonsterName { get; set; }

    public Challenge(List<string> dialog, string tagType, string monsterName)
    {
        Dialog = new Queue<string>(dialog);
        TagType = tagType;
        MonsterName = monsterName;
    }
}