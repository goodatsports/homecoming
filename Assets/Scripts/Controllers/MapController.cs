using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;

public class MapController : MonoBehaviour
{
    public GridLayout Map;
    public TileBase GrassTile;

    private Tilemap _groundMap;
    private Tilemap _obstacleMap;

    public Tilemap ObstacleMap {
        get => _obstacleMap;
        private set => _obstacleMap = value; }
    public Tilemap GroundMap {
        get => _groundMap;
        private set => _groundMap = value; }

    private Dictionary<Vector3Int, TreeTile> TreeMap;

    void Awake()
    {
        // Get references to child maps and instantiate TreeMap
        Map = gameObject.GetComponent<GridLayout>();
        GroundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
        ObstacleMap = GameObject.Find("Walls").GetComponent<Tilemap>();
        TreeMap = new Dictionary<Vector3Int, TreeTile>();

        if (ObstacleMap == null || GroundMap == null) print("Maps not found!");

        // Add all TreeTiles to TreeMap
        foreach (Vector3Int pos in ObstacleMap.cellBounds.allPositionsWithin) {   
            TreeTile possibleTile = ObstacleMap.GetTile<TreeTile>(pos);
            if (possibleTile != null) {
                TreeMap[pos] = new TreeTile();
            }
        }
    }

    public void ChopTree(Vector3Int address)
    {
        TreeTile target = TreeMap[address];

        if (target != null) {
            target.Health--;
        }
        else return;

        // If tree has no health, remove tile
        if (target.Health == 0) {
            GameEvents.current.TreeChopped();
            ObstacleMap.SetTile(address, null);
        }
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
