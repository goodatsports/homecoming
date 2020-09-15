using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile", menuName = "TreeTile")]
public class TreeTile : CustomTile
{
    MapController Map;
    void OnEnable()
    {
        // check with scriptable object stuff, this is not being tied to the specific tile, position
        Map = GameObject.Find("Map").GetComponentInChildren(typeof(MapController)) as MapController;
    }

    public override void Interact()
    {
        Debug.Log($" Tree tile ({Address}) reporting from Position ({Pos}) updating...");
        Map.ChopTree(Address);
        
    }

}
