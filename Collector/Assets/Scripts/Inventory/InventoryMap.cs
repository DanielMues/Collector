using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMap: MonoBehaviour
{
    public long cellSizeX = 2;
    public long cellSizeY = 2;
    public int cellAmountX = 2;
    public int cellAmountY = 7;
    public Vector3 worldPosition;
    public GameObject backgroundTilePrefab;
    private Map map;
    // Start is called before the first frame update
    void Start()
    {
        map = new Map(worldPosition, cellAmountX, cellAmountY, cellSizeX, cellSizeY);
        for (int x = 0; x < cellAmountX; x++)
        {
            for (int y = 0; y < cellAmountY; y++)
            {
                Vector3 position = map.mapGrid[x, y].centerPosition;
                GameObject Tile = GameObject.Instantiate(backgroundTilePrefab);
                Tile.transform.position = position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
