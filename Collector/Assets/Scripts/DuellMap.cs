using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInformation
{
    public int gridNumber;
    public GameObject unit;
    public long xPosition;
    public long yPosition;
}

public class DuellMap : MonoBehaviour
{
    private int mapSizeX = 8;
    private int mapSizeY = 8;
    private long xPositionNew = 0;
    private long yPositionNew = 0;
    private MapInformation[,] mapGrid; 
    // Start is called before the first frame update
    void Start()
    {
        mapGrid = new MapInformation[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                mapGrid[x, y].gridNumber = x + y;
                mapGrid[x, y].unit = null;
                mapGrid[x, y].xPosition = xPositionNew;
                mapGrid[x, y].yPosition = yPositionNew;
                yPositionNew += 200;
            }
            xPositionNew += 200;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
