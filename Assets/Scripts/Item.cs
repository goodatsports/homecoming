using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name;
    public string Description;
    public int Value;
    public Sprite sprite;

    public Item(string name, string description, int value) {
        this.Name = name;
        this.Description = description;
        this.Value = value;
    }
}

