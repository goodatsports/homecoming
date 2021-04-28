using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class MapController : MonoBehaviour
{
    public GridLayout Map;
    public GameObject TreePrefab;


    [SerializeField]
    public TileBase GrassTile;

    [SerializeField]
    public TileBase RopeEndTile;

    [Header("Rope tiles")]
    public TileBase LeftRightRope;
    public TileBase UpDownRope;

    private Tilemap _groundMap;
    private Tilemap _obstacleMap;
    private Tilemap _aboveGroundMap;

    public Tilemap ObstacleMap {
        get => _obstacleMap;
        private set => _obstacleMap = value; }
    public Tilemap GroundMap {
        get => _groundMap;
        private set => _groundMap = value; }

    public Tilemap AboveGroundMap {
        get => _aboveGroundMap;
        private set => _aboveGroundMap = value;
    }

    private Dictionary<Vector3Int, TreeController> TreeMap;
    private HashSet<Vector3Int> RopeMap;

    // Offset to center Tile sprite transform
    private Vector3 TILE_OFFSET = new Vector3(0.5f, 0.5f);

    void Awake() {
        // Get references to child maps and instantiate TreeMap + RopeMap
        Map = gameObject.GetComponent<GridLayout>();
        GroundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
        ObstacleMap = GameObject.Find("Walls").GetComponent<Tilemap>();
        AboveGroundMap = GameObject.Find("Above Ground").GetComponent<Tilemap>();
        TreeMap = new Dictionary<Vector3Int, TreeController>();
        RopeMap = new HashSet<Vector3Int>();

        if (ObstacleMap == null || GroundMap == null || AboveGroundMap == null) print("Maps not found!");

        // Parent object for all tree objects; just used to clean up the Hierarchy view
        GameObject TreesContainer = new GameObject("Trees");

        // Add all RopeableTiles to RopeMap
        foreach (Vector3Int pos in GroundMap.cellBounds.allPositionsWithin) {
            RopeableTile possibleTile = GroundMap.GetTile<RopeableTile>(pos);
            if (possibleTile != null) {
                RopeMap.Add(pos);
            }
        }

        // Add all TreeTiles to TreeMap and TreesContainer
        foreach (Vector3Int pos in ObstacleMap.cellBounds.allPositionsWithin) {
            TreeTile possibleTile = ObstacleMap.GetTile<TreeTile>(pos);
            if (possibleTile != null) {
                GameObject go = Instantiate(TreePrefab, CellToWorld(pos) + TILE_OFFSET, Quaternion.identity);
                go.transform.parent = TreesContainer.transform;

                // Place object on Character layer
                go.layer = 8;

                TreeController newTree = go.GetComponent<TreeController>();
                newTree.Tile = ScriptableObject.CreateInstance<Tile>();
                TreeMap[pos] = newTree;

            }
        }

    }

    public void ChopTree(Vector3Int address)
    {
        GameEvents.current.TreeChopped();
        ObstacleMap.SetTile(address, null);
        TreeMap[address] = null;
    }

    public void PaintRope(Vector3Int address, Vector3 dir) {
        if (dir == Vector3.up || dir == Vector3.down) {
            AboveGroundMap.SetTile(address, UpDownRope);
        } else if (dir == Vector3.left || dir == Vector3.right) {
            AboveGroundMap.SetTile(address, LeftRightRope);
        }
    }

    public void ClearRopeTile(Vector3Int address) {
        AboveGroundMap.SetTile(address, null);
    }

    public Vector3Int WorldToCell(Vector3 position)
    {
        return Map.WorldToCell(position);
    }

    public Vector3 CellToWorld(Vector3Int address) {
        return Map.CellToWorld(address);
    }

    public bool HasObstacle(Vector3 address) {
        return ObstacleMap.HasTile(WorldToCell(address));
    }

    public bool HasRopeable(Vector3Int address) {
        return RopeMap.Contains(address);
    }
}
