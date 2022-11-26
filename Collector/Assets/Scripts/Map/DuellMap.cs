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
    
    public void GetXY(out int x, out int y, Vector3 position)
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
        return mapGrid[x, y].centerPosition + new Vector3(0, 0, -1);
    }

    public Vector3 GetCenteredPosition(int x, int y)
    {
        return mapGrid[x, y].centerPosition + new Vector3(0, 0, -1);
    }

    public void SetUnit(GameObject unit, Vector3 position)
    {
        int x, y;
        GetXY(out x, out y, position);
        mapGrid[x, y].unit = unit;
    }

    public void SetUnit(GameObject unit, int x, int y)
    {
        mapGrid[x, y].unit = unit;
    }

    public void DeleteUnit (Vector3 position)
    {
        int x, y;
        GetXY(out x, out y, position);
        mapGrid[x, y].unit = null;
    }

    public GameObject GetUnit(Vector3 position)
    {
        int x, y;
        GetXY(out x, out y, position);
        return mapGrid[x, y].unit;
    }

    public GameObject GetUnit(int x, int y)
    {
        return mapGrid[x, y].unit;
    }
}

public class DuellMap : MonoBehaviour
{
    // Eventhandlers
    private CustomEventHandler customEventHandler;
    private FightEventHandler fightEventHandler;
    // map
    public long sizeX = 2;
    public long sizeY = 2;
    private int xSize = 8;
    private int ySize = 8;
    public GameObject backgroundTilePrefab;
    public Vector3 worldPosition = new Vector3(0, 0, 0);
    private Map map;

    // hoverTile
    public GameObject hoverTile;
    private GameObject tile;
    private bool hoverActivated;
    
    // fight
    private bool fightActivated;
    public float timeBetweenListUpdates = 1f;
    private float currentTimeBetweenUpdates;

    // Start is called before the first frame update
    void Start()
    {
        // EventHandler
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.PlaceUnitOnField += PlaceUnit;
        customEventHandler.DeleteUnitFromField += DeleteUnitOnField;
        fightEventHandler = FightEventHandler.instance;
        fightEventHandler.MoveUnit += MoveUnitX;
        // hoverTile
        hoverActivated = true;
        tile = GameObject.Instantiate(hoverTile);
        // map
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
        //fight
        fightActivated = false;
        currentTimeBetweenUpdates = timeBetweenListUpdates;
    }

    // Update is called once per frame
    void Update()
    {
        if (fightActivated && currentTimeBetweenUpdates <= 0)
        {
            fightEventHandler.UpdateUnitList(GetAllUnits());
            currentTimeBetweenUpdates = timeBetweenListUpdates;
        }
        else if (hoverActivated)
        {
            hoverTheTile();
        }
        currentTimeBetweenUpdates -= Time.deltaTime;
    }

    private void hoverTheTile()
    {
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tile.transform.position = map.GetCenteredPosition(currentMousePosition);
    }

    private void PlaceUnit(object sender, CustomEventHandler.UnitInformation args)
    {
        GameObject currentUnit = map.GetUnit(args.worldPosition);
        if (currentUnit == null || currentUnit == args.unit)
        {
            map.SetUnit(args.unit, args.worldPosition);
            args.unit.transform.position = map.GetCenteredPosition(args.worldPosition);
        }
        else
        {
            map.SetUnit(args.unit, args.worldPosition);
            Vector3 currentMapPosition = map.GetCenteredPosition(args.worldPosition);
            args.unit.transform.position = currentMapPosition;
            customEventHandler.SwapSelectedUnit(currentUnit, currentMapPosition);
        }
    }

    private void DeleteUnitOnField(object sender, CustomEventHandler.UnitInformation args)
    {
        if(args.unit == map.GetUnit(args.worldPosition))
        {
            map.DeleteUnit(args.worldPosition);
            Debug.Log("deleted");
        }
        Debug.Log(map.GetUnit(args.worldPosition));
    }

    public void SetUnitOnField(GameObject unit)
    {
        hoverActivated = true;
    }

    //fighting algorithms

    public void StartFight()
    {
        hoverActivated = false;
        fightActivated = true;
        fightEventHandler.StartTheFight();
        customEventHandler.DeactivateDragAndDrop();
    }

    private List<GameObject> GetAllUnits()
    {
        List<GameObject> unitsOnField = new List<GameObject>();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject currentUnit = map.GetUnit(x, y);
                if(currentUnit != null)
                {
                    unitsOnField.Add(currentUnit);
                }
            }
        }
        return unitsOnField;
    }

    private void MoveUnitX(object sender, FightEventHandler.UnitMovement args)
    {
        int currentX = 0;
        int currentY = 0;
        GameObject currentUnit = args.GetUnit();
        map.GetXY(out currentX, out currentY, currentUnit.transform.position);
        GetNewXYPosition(args.GetY(), args.GetUp(), args.GetSteps(), ref currentX, ref currentY);
        if (map.GetUnit(currentX, currentY) == null)
        {
            setUnitAndFreeOldSpace(currentUnit, currentX, currentY);
        }
        else if (args.GetIfSpecialMove())
        {
            while(map.GetUnit(currentX, currentY) == null && (currentX != currentUnit.transform.position.x || currentY != currentUnit.transform.position.y) )
            {
                GetNewXYPosition(args.GetY(), args.GetUp(), -1, ref currentX, ref currentY);
            }
            if(map.GetUnit(currentX, currentY) != null)
            {
                setUnitAndFreeOldSpace(currentUnit, currentX, currentY);
            }
        }
    }

    private void setUnitAndFreeOldSpace(GameObject currentUnit, int x, int y)
    {
        map.SetUnit(currentUnit, x, y);
        map.DeleteUnit(currentUnit.transform.position);
        currentUnit.transform.position = map.GetCenteredPosition(x, y);
    }

    private void GetNewXYPosition(bool isY, bool isPositive, int steps, ref int x, ref int y)
    {
        if (isY)
        {
            if (isPositive)
            {
                y += steps;
            }
            else
            {
                y -= steps;
            }
        }
        else
        {
            if (isPositive)
            {
                x += steps;
            }
            else
            {
                x -= steps;
            }
        }
    }
    

}
