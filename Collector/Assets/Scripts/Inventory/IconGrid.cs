using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconGrid : MonoBehaviour
{
    public float cellSizeX = 1f;
    public float cellSizeY = 1f;
    public int cellAmountX = 7;
    public int cellAmountY = 1;
    public Vector3 worldPosition;
    public GameObject backgroundTilePrefab;
    private Map map;

    public void Start()
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
}
