using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public int Value;
    public Sprite Sprite;

    public Item(string name, string description, int value) {
        this.Name = name;
        this.Description = description;
        this.Value = value;
    }
}

