using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllInventoryMap: MonoBehaviour
{
    public float cellSizeX = 1f;
    public float cellSizeY = 1f;
    public int cellAmountX = 10;
    public int cellAmountY = 10;
    public Vector3 worldPosition;
    public GameObject backgroundTilePrefab;
    private Map map;
    private CustomEventHandler customEventHandler;
    public List<GameObject> allMutants;

    // init map
    public List<Mutant> playerMutants;
    // Start is called before the first frame update
    void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        //customEventHandler.PlaceUnitOnField += PlaceUnit;
        //customEventHandler.DeleteUnitFromField += DeleteUnitOnField;
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
        InitInventory();
    }

    private void InitInventory()
    {
        int currentX = 0;
        int currentY = 0;
        
    }

    //private void PlaceUnit(object sender, CustomEventHandler.UnitInformation args)
    //{
    //    if (map.IsPositionOnMap(args.worldPosition, 0, cellAmountX, 0 , cellAmountY))
    //    {
    //        try
    //        {
    //            GameObject currentUnit = map.GetUnit(args.worldPosition);
    //            if (currentUnit == null || currentUnit == args.unit)
    //            {
    //                map.SetUnit(args.unit, args.worldPosition);
    //                args.unit.transform.position = map.GetCenteredPosition(args.worldPosition);
    //                customEventHandler.UnitSet();
    //            }
    //            else
    //            {
    //                map.SetUnit(args.unit, args.worldPosition);
    //                Vector3 currentMapPosition = map.GetCenteredPosition(args.worldPosition);
    //                args.unit.transform.position = currentMapPosition;
    //                customEventHandler.SwapSelectedUnit(currentUnit, currentMapPosition);
    //            }
    //        }
    //        catch (IndexOutOfRangeException)
    //        {
    //            Debug.LogWarning("Cant place Unit out of Range");
    //        }
    //    }
    //}

    //private void DeleteUnitOnField(object sender, CustomEventHandler.UnitInformation args)
    //{
    //    if (map.IsPositionOnMap(args.worldPosition, 0, cellAmountX, 0, cellAmountY))
    //    {
    //        try
    //        {
    //            GameObject currentUnit = map.GetUnit(args.worldPosition);
    //            if (args.unit == currentUnit)
    //            {
    //                map.DeleteUnit(args.worldPosition);
    //            }
    //        }
    //        catch (IndexOutOfRangeException)
    //        {
    //            Debug.LogWarning("position out of bounds");
    //        }
    //    }
    //}
}
