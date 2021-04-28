using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName ="CustomTile")]
public class CustomTile : Tile
{
    public virtual void Interact()
    {
        Debug.Log($"tile ({name}) reporting");

    }
}
