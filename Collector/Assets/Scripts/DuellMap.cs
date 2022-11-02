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
    private int mapSizeX;
    private int mapSizeY;
    private long cellSizeX;
    private long cellSizeY;
    private Vector3 worldPosition;
    public MapInformation[,] mapGrid;


    public Map (Vector3 worldPosition, int sizeX, int sizeY, long cellSizeX, long cellSizeY)
    {
        this.cellSizeX = cellSizeX;
        this.cellSizeY = cellSizeY;
        this.mapSizeX = sizeX;
        this.mapSizeY = sizeY;
        this.worldPosition = worldPosition;
        mapGrid = new MapInformation[mapSizeX, mapSizeY];
        if (mapGrid != null)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                for (int y = 0; y < mapSizeY; y++)
                {
                    Vector3 temp = (new Vector3(x * cellSizeX, y* cellSizeY, 0) + worldPosition);
                    mapGrid[x, y] = new MapInformation((x * mapSizeX) + y, null, temp + new Vector3(cellSizeX/2, cellSizeY/2, 0));
                }
            }
        }
        else
        {
            Debug.Log("No grid available");
        }
    }
    
    private void GetXY(out int x, out int y, Vector3 position)
    {
        x = Mathf.FloorToInt((position - worldPosition).x / cellSizeX);
        y = Mathf.FloorToInt((position - worldPosition).y / cellSizeY);
        if (x < 0)
        {
            x = 0;
        }
        else if (x > mapSizeX - 1)
        {
            x = mapSizeX - 1;
        }

        if (y < 0)
        {
            y = 0;
        }
        else if (y > mapSizeY - 1)
        {
            y = mapSizeY - 1;
        }
    }

    public Vector3 GetCenteredPosition(Vector3 position)
    {
        int x, y;
        GetXY(out x, out y, position);
        return mapGrid[x, y].centerPosition;
    }

    public void SetUnit(GameObject unit, Vector3 position)
    {
        int x, y;
        GetXY(out x, out y, position);
        mapGrid[x, y].unit = unit;
    }

    public void DeleteUnit (Vector3 position)
    {
        int x, y;
        GetXY(out x, out y, position);
        mapGrid[x, y].unit = null;
    }
}

public class DuellMap : MonoBehaviour
{
    public long sizeX = 2;
    public long sizeY = 2;
    private int xSize = 8;
    private int ySize = 8;
    public GameObject backgroundTilePrefab;
    public GameObject hoverTile;
    public Vector3 worldPosition = new Vector3(0,0,0);
    private Map map;
    private GameObject tile;

    private bool hoverActivated;
    private GameObject currentSelectedUnit;
    private CustomEventHandler customEventHandler;


    // Start is called before the first frame update
    void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.PlaceUnitOnField += PlaceUnit;
        hoverActivated = true;
        currentSelectedUnit = null;
        map = new Map(worldPosition, xSize, ySize, sizeX, sizeY);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Vector3 position = map.mapGrid[x, y].centerPosition;
                GameObject Tile = GameObject.Instantiate(backgroundTilePrefab);
                Tile.transform.position = position;
            }
        }
        tile = GameObject.Instantiate(hoverTile);
    }

    // Update is called once per frame
    void Update()
    {
        if (hoverActivated)
        {
            hoverTheTile();
        }
        
    }

    private void hoverTheTile()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tile.transform.position = map.GetCenteredPosition(currentMousePosition);
    }

    private void PlaceUnit(object sender, CustomEventHandler.PlaceUnit args)
    {
        map.SetUnit(args.unit, args.worldPosition);
        args.unit.transform.position = map.GetCenteredPosition(args.worldPosition) + new Vector3(0, 0, -1) ;
    }

    public void SetUnitOnField(GameObject unit)
    {
        hoverActivated = true;
    }
}
