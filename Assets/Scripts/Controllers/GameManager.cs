using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public MapController Map;
    public Tilemap ObstacleMap;
    private int TreesChopped = 0;
    private int MAX_TREES;

    private void Awake() {
        GameEvents.current.onTreeChopped += TreeChopped;
    }

    // Start is called before the first frame update
    void Start()
    {
        ObstacleMap = Map.ObstacleMap;
        MAX_TREES = GetAllTreeTiles();
    }

   void TreeChopped() {
        TreesChopped++;
        print("trees chopped: " + TreesChopped);
        if (AllTreesChopped()) {
            print("ALL TREES CHOPPED");
        }
    }

    int GetAllTreeTiles() {
        int treeTileCount = 0;
        BoundsInt bounds = ObstacleMap.cellBounds;

        for (int i = bounds.xMin; i < bounds.xMax; i++) {
            for (int j = bounds.yMin; j < bounds.yMax; j++) {
                if (ObstacleMap.GetTile<TreeTile>(new Vector3Int(i, j, 0)) != null) {
                treeTileCount++;
                }
            }
        }
        return treeTileCount;
    }

    public bool AllTreesChopped() {
        if (TreesChopped == MAX_TREES) return true;
        else return false;
    }
}
