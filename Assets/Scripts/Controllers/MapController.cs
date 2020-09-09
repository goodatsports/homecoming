using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;

public class MapController : MonoBehaviour
{
    public GridLayout Map;
    private Tilemap _groundMap;
    private Tilemap _obstacleMap;

    public Tilemap ObstacleMap {
        get => _obstacleMap;
        private set => _obstacleMap = value; }
    public Tilemap GroundMap {
        get => _groundMap;
        private set => _groundMap = value; }

    void Awake()
    { 
        Map = gameObject.GetComponent<GridLayout>();
        GroundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
        ObstacleMap = GameObject.Find("Walls").GetComponent<Tilemap>();


        if (ObstacleMap == null || GroundMap == null) print("Maps not found!");

        //Set address and position for all tiles in ObstacleMap
        foreach (Vector3Int pos in ObstacleMap.cellBounds.allPositionsWithin)
        {
            Vector3Int address = new Vector3Int(pos.x, pos.y, pos.z);
            if (ObstacleMap.HasTile(address))
            {
         
        //        //Type tileType = currentTile.GetType();
        //        //CustomTile tile = ScriptableObject.CreateInstance<CustomTile>();
        //        Vector3 tilePos = Map.CellToWorld(address);
        //        tile.Pos = tilePos;
        //        tile.Address = address;
        //        tile.sprite = GetTileSprite(ObstacleMap.GetTile<Tile>(address));
        //        ObstacleMap.SetTile(address, tile);
            }
        }
    }

    // based off of sprite on tilemap, should make new function that
    // doesn't care about sprite, just the type of tile
    public Sprite GetTileSprite(Tile tile)
    {
        if (tile.sprite == null)
        {
            Debug.Log("tile does not have sprite");
            return null;
        }
        else return tile.sprite;
    }

    public void ChopTree(Vector3Int address)
    {
        CustomTile currentTile = ObstacleMap.GetTile<TreeTile>(address);
        GrassTile newTile = ScriptableObject.CreateInstance<GrassTile>();

        // ground tile does update, but the sprite field is empty...
        //GroundMap.SetTile(address, newTile);
        ObstacleMap.SetTile(address, null);
        Debug.Log("TREE CHOPPED");

    }
    
    public Tilemap GetGroundMap()
    {
        return GroundMap;
    }

    public Tilemap GetObstacleMap()
    {
        return ObstacleMap;
    }

    public Vector3Int WorldToCell(Vector3 position)
    {
        return Map.WorldToCell(position);
    }
}
