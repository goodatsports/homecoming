using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName ="CustomTile")]
public class CustomTile : Tile
{
    private Vector3 pos;
    private Vector3Int address;
    
    public Vector3 Pos { get => pos; set => pos = value; }
    public Vector3Int Address { get => address; set => address = value; }


    public virtual void Interact()
    {
        Debug.Log($"tile ({name}) reporting from Position ({Pos})");

    }


}
