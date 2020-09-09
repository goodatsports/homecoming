using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "GrassTile")]
public class GrassTile : CustomTile
{


    public override void Interact()
    {
        Debug.Log($"Grass tile ({name}) reporting from Position ({Pos})");

    }


}