using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavManager : MonoBehaviour
{
    public static NavManager Instance;

    [SerializeField]
    private Tilemap terrainMap;
    [SerializeField]
    public Tilemap navMap;
    [SerializeField]
    private TileBase occupiedTile;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMoveDisplacement(BaseUnit entity, Vector2 displacement, Vector3Int navDist)
    {
        Vector2 currPos = entity.transform.position;
        Vector2 newPos = currPos + displacement;
        
        // if the unit is on the same original tile, proceed with the movement
        var nextPosTilePos = navMap.WorldToCell(newPos);
        if (nextPosTilePos == entity.tilePos)
        {
            // never snap if we are moving away from the current tile
            if (nextPosTilePos == navDist && ShouldSnapToGrid(currPos, newPos, nextPosTilePos))
            {
                return (Vector2)navMap.CellToWorld(nextPosTilePos) - currPos;
            }
            return displacement;
        }

        // if the unit will move to an occupied tile, block the movement
        if (navMap.GetTile(navMap.WorldToCell(newPos)) == occupiedTile)
        {
            return Vector2.zero;
        }
        
        // proceed to move to a different tile
        navMap.SetTile(entity.tilePos, null);
        entity.tilePos = navMap.WorldToCell(newPos);
        if (ShouldSnapToGrid(currPos, newPos, nextPosTilePos))
        {
            return (Vector2)navMap.CellToWorld(nextPosTilePos) - currPos;
        }
        
        print("placed down new tiles");
        navMap.SetTile(nextPosTilePos, occupiedTile);
        return displacement;
    }

    private bool ShouldSnapToGrid(Vector2 currPos, Vector2 newPos, Vector3Int tilePos)
    {
        Vector2 tilePosWorld = navMap.CellToWorld(tilePos);
        return Vector2.Dot(newPos - currPos, tilePosWorld - currPos) < 0;
    }
}
