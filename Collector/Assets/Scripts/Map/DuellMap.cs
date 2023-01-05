using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DuellMap : MonoBehaviour
{
    // Eventhandlers
    private CustomEventHandler customEventHandler;
    private FightEventHandler fightEventHandler;
    private TypeAndClassEventHandler typeAndClassEventHandler;
    // map
    public float sizeX = 2f; // size of cell in x direction
    public float sizeY = 2f; // size of cell in y direction
    public int xSize = 8; // amount of cells in x direction
    public int ySize = 8; // amount of cells in y direction
    public GameObject backgroundTilePrefab;
    public Vector3 worldPosition = new Vector3(0, 0, 0);
    private Map map;

    // hoverTile
    public GameObject hoverTile;
    private GameObject tile;
    private bool hoverActivated;
    
    // fight
    private bool fightActivated;
    public float timeBetweenListUpdates = 0.05f;
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
        typeAndClassEventHandler = TypeAndClassEventHandler.instance;
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
        try
        {
            tile.transform.position = map.GetCenteredPosition(currentMousePosition);
            if (!tile.activeSelf)
            {
                tile.SetActive(true);
            }
        }
        catch (IndexOutOfRangeException)
        {
            if (tile.activeSelf)
            {
                tile.SetActive(false);
            }
        }
        
    }

    private void PlaceUnit(object sender, CustomEventHandler.UnitInformation args)
    {
        if (map.IsPositionOnMap(args.worldPosition, 0 , xSize/2, 0, ySize))
        {
            try
            {
                GameObject currentUnit = map.GetUnit(args.worldPosition);
                if (currentUnit == null || currentUnit == args.unit)
                {
                    map.SetUnit(args.unit, args.worldPosition);
                    typeAndClassEventHandler.setUnitTypeAndClass(args.unit.GetComponent<UnitStats>().GetFirstUnitType(), args.unit.GetComponent<UnitStats>().GetSecondUnitType(), args.unit.GetComponent<UnitStats>().GetThirdUnitType(), args.unit.GetComponent<UnitStats>().GetTeam());
                    args.unit.transform.position = map.GetCenteredPosition(args.worldPosition);
                }
                else
                {
                    map.SetUnit(args.unit, args.worldPosition);
                    typeAndClassEventHandler.setUnitTypeAndClass(args.unit.GetComponent<UnitStats>().GetFirstUnitType(), args.unit.GetComponent<UnitStats>().GetSecondUnitType(), args.unit.GetComponent<UnitStats>().GetThirdUnitType(), args.unit.GetComponent<UnitStats>().GetTeam());
                    Vector3 currentMapPosition = map.GetCenteredPosition(args.worldPosition);
                    args.unit.transform.position = currentMapPosition;
                    customEventHandler.SwapSelectedUnit(currentUnit, currentMapPosition);
                    typeAndClassEventHandler.deleteUnitTypeAndClass(currentUnit.GetComponent<UnitStats>().GetFirstUnitType(), currentUnit.GetComponent<UnitStats>().GetSecondUnitType(), currentUnit.GetComponent<UnitStats>().GetThirdUnitType(), currentUnit.GetComponent<UnitStats>().GetTeam());
                }
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogWarning("Cant place Unit out of Range");
            }
        }
        else
        {
            customEventHandler.UnitCouldNotBeSet(args.unit, args.worldPosition);
        }
    }

    private void DeleteUnitOnField(object sender, CustomEventHandler.UnitInformation args)
    {
        if (map.IsPositionOnMap(args.worldPosition, 0 , xSize, 0, ySize))
        {
            try
            {
                GameObject currentUnit = map.GetUnit(args.worldPosition);
                if (args.unit == currentUnit)
                {
                    map.DeleteUnit(args.worldPosition);
                    typeAndClassEventHandler.deleteUnitTypeAndClass(currentUnit.GetComponent<UnitStats>().GetFirstUnitType(), currentUnit.GetComponent<UnitStats>().GetSecondUnitType(), currentUnit.GetComponent<UnitStats>().GetThirdUnitType(), currentUnit.GetComponent<UnitStats>().GetTeam());
                }
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogWarning("position out of bounds");
            }
        }
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
        typeAndClassEventHandler.createTeams();
    }

    private List<GameObject> GetAllUnits()
    {
        List<GameObject> unitsOnField = new List<GameObject>();
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                try
                {
                    GameObject currentUnit = map.GetUnit(x, y);
                    if (currentUnit != null)
                    {
                        unitsOnField.Add(currentUnit);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.LogWarning("mapposition not on field");
                }
            }
        }
        return unitsOnField;
    }

    private void MoveUnitX(object sender, FightEventHandler.UnitMovement args)
    {
        int currentX = 0;
        int currentY = 0;
        try
        {
            GameObject currentUnit = args.GetUnit();
            map.GetXY(out currentX, out currentY, currentUnit.transform.position);
            GetNewXYPosition(args.GetY(), args.GetUp(), args.GetSteps(), ref currentX, ref currentY);
            if (map.GetUnit(currentX, currentY) == null)
            {
                setUnitAndFreeOldSpace(currentUnit, currentX, currentY);
            }
            else if (args.GetIfSpecialMove())
            {
                while (map.GetUnit(currentX, currentY) == null && (currentX != currentUnit.transform.position.x || currentY != currentUnit.transform.position.y))
                {
                    GetNewXYPosition(args.GetY(), args.GetUp(), -1, ref currentX, ref currentY);
                }
                if (map.GetUnit(currentX, currentY) != null)
                {
                    setUnitAndFreeOldSpace(currentUnit, currentX, currentY);
                }
            }
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogWarning("index out of range");
        }
    }

    private void setUnitAndFreeOldSpace(GameObject currentUnit, int x, int y)
    {
        map.SetUnit(currentUnit, x, y);
        map.DeleteUnit(currentUnit.transform.position);
        try
        {
            currentUnit.transform.position = map.GetCenteredPosition(x, y);
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogWarning("positon of unit is out of bounds");
        }
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
