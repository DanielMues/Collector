using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private int mapSizeX;  // amount of cells in x direction
    private int mapSizeY; // amount of cells in y direction
    private float cellSizeX; // size of cell in x direction
    private float cellSizeY; // size of cell in y direction 
    private Vector3 worldPosition;
    public MapInformation[,] mapGrid;


    public Map(Vector3 worldPosition, int sizeX, int sizeY, float cellSizeX, float cellSizeY)
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
                    Vector3 temp = (new Vector3(x * cellSizeX, y * cellSizeY, 0) + worldPosition);
                    mapGrid[x, y] = new MapInformation((x * mapSizeX) + y, null, temp + new Vector3(cellSizeX / 2, cellSizeY / 2, 0));
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
            throw new IndexOutOfRangeException();
        }
        else if (x > mapSizeX - 1)
        {
            throw new IndexOutOfRangeException();
        }

        if (y < 0)
        {
            throw new IndexOutOfRangeException();
        }
        else if (y > mapSizeY - 1)
        {
            throw new IndexOutOfRangeException();
        }
    }

    public Vector3 GetCenteredPosition(Vector3 position)
    {
        int x, y;
        try
        {
            GetXY(out x, out y, position);
            return mapGrid[x, y].centerPosition + new Vector3(0, 0, -1);
        }
        catch (IndexOutOfRangeException)
        {
            throw new IndexOutOfRangeException();
        }
    }

    public Vector3 GetCenteredPosition(int x, int y)
    {
        return mapGrid[x, y].centerPosition + new Vector3(0, 0, -1);
    }

    public void SetUnit(GameObject unit, Vector3 position)
    {
        int x, y;
        try
        {
            GetXY(out x, out y, position);
            mapGrid[x, y].unit = unit;
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogWarning("cant set units out of bounds");
        }
    }

    public void SetUnit(GameObject unit, int x, int y)
    {
        mapGrid[x, y].unit = unit;
    }

    public void DeleteUnit(Vector3 position)
    {
        int x, y;
        try
        {
            GetXY(out x, out y, position);
            mapGrid[x, y].unit = null;
        }
        catch (IndexOutOfRangeException)
        {
            Debug.LogWarning("can´t delete unit out of bounds");
        }
    }

    public GameObject GetUnit(Vector3 position)
    {
        int x, y;
        try
        {
            GetXY(out x, out y, position);
            return mapGrid[x, y].unit;
        }
        catch (IndexOutOfRangeException)
        {
            throw new IndexOutOfRangeException();
        }

    }

    public GameObject GetUnit(int x, int y)
    {
        return mapGrid[x, y].unit;
    }

    public bool IsPositionOnMap(Vector3 position, int xMin, int xMax, int yMin, int yMax)
    {
        int x = Mathf.FloorToInt((position - worldPosition).x / cellSizeX);
        int y = Mathf.FloorToInt((position - worldPosition).y / cellSizeY);
        if (x < xMin)
        {
            return false;
        }
        else if (x > xMax - 1)
        {
            return false;
        }

        if (y < yMin)
        {
            return false;
        }
        else if (y > yMax - 1)
        {
            return false;
        }

        return true;
    }
}