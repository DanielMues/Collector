using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInformation
{
    public int gridNumber;
    public GameObject unit;
    public Vector3 centerPosition;

    public MapInformation(int number, GameObject unit, Vector3 position)
    {
        this.gridNumber = number;
        this.unit = unit;
        this.centerPosition = position;
    }
}

public class Map
{
    private int mapSizeX = 8;
    private int mapSizeY = 8;
    private long cellSize = 5;
    private Vector3 worldPosition;
    public MapInformation[,] mapGrid;


    public Map (Vector3 worldPosition, int sizeX, int sizeY, long cellSize)
    {
        this.cellSize = cellSize;
        mapSizeX = sizeX;
        mapSizeY = sizeY;
        this.worldPosition = worldPosition;
        mapGrid = new MapInformation[mapSizeX, mapSizeY];
        if (mapGrid != null)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    mapGrid[x, y] = new MapInformation((x * mapSizeX) + y, null, (new Vector3(x, y, 0) * cellSize / 2) + worldPosition);
                }
            }
        }
        else
        {
            Debug.Log("No grid available");
        }
    }
    
    public Vector3 GetCenteredPosition(Vector3 position)
    {
        int x = Mathf.FloorToInt((position - worldPosition).x / cellSize);
        int y = Mathf.FloorToInt((position - worldPosition).y / cellSize);

        return mapGrid[x, y].centerPosition;
    }
}

public class DuellMap : MonoBehaviour
{
    public long size = 5;
    private int xSize = 8;
    private int ySize = 8;
    public GameObject hovertilePrefab;
    public Vector3 worldPosition = new Vector3(0,0,0);
    private Map map;

    


    // Start is called before the first frame update
    void Start()
    {
        map = new Map(worldPosition, xSize, ySize, size);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = map.mapGrid[x, y].centerPosition;
                GameObject Tile = GameObject.Instantiate(hovertilePrefab);
                Tile.transform.position = position;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // hovertile.transform.position = map.GetCenteredPosition(currentMousePosition);
    }
}
