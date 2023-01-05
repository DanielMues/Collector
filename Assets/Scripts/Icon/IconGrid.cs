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
    private Map iconBar;
    private TypeAndClassEventHandler typeAndClassEventHandler;
    public List<GameObject> iconPrefabs;
    public string team;

    public void Start()
    {
        iconBar = new Map(worldPosition, cellAmountX, cellAmountY, cellSizeX, cellSizeY);
        for (int x = 0; x < cellAmountX; x++)
        {
            for (int y = 0; y < cellAmountY; y++)
            {
                Vector3 position = iconBar.mapGrid[x, y].centerPosition;
                GameObject Tile = GameObject.Instantiate(backgroundTilePrefab);
                Tile.transform.position = position;
            }
        }
        typeAndClassEventHandler = TypeAndClassEventHandler.instance;
        typeAndClassEventHandler.sendIconUpdate += UpdateIconBar;
    }

    private void UpdateIconBar(object sender, TypeAndClassEventHandler.TypeBuffInformation args)
    {
        if(args.GetTeam() == team && FindIcon(args.GetTypeType()) && args.GetAmount() == 0)
        {
            DeleteIcon(GetIcon(args.GetTypeType()));
        }
        else if (args.GetTeam() == team && !FindIcon(args.GetTypeType()) && args.GetAmount() > 0)
        {
            AddIcon(GetIcon(args.GetTypeType()));
        }
    }

    private GameObject GetIcon(TypeAndClassHandler.unitType type)
    {
        foreach (GameObject icon in iconPrefabs)
        {
            if (icon.GetComponent<Icontype>().type == type)
            {
                return icon;
            }
        }
        return null;
    }

    private bool FindIcon(TypeAndClassHandler.unitType type)
    {
        for (int x = 0; x < cellAmountX; x++)
        {
            for (int y = 0; y < cellAmountY; y++)
            {
                if (iconBar.mapGrid[x, y].unit != null && iconBar.mapGrid[x, y].unit.GetComponent<Icontype>().type == type)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void AddIcon(GameObject iconPrefab)
    {
        for (int x = 0; x < cellAmountX; x++)
        {
            for (int y = 0; y < cellAmountY; y++)
            {
                if (iconBar.mapGrid[x, y].unit == null)
                {
                    GameObject newIcon = GameObject.Instantiate(iconPrefab);
                    iconBar.mapGrid[x, y].unit = newIcon;
                    newIcon.transform.position = iconBar.mapGrid[x, y].centerPosition;
                    return;
                }
            }
        }
    }

    private void DeleteIcon(GameObject iconPrefab)
    {
        bool deleted = false;
        int emptyX = 0;
        int emptyY = 0;
        for (int x = 0; x < cellAmountX; x++)
        {
            for (int y = 0; y < cellAmountY; y++)
            {
                if (iconBar.mapGrid[x, y].unit != null && iconBar.mapGrid[x, y].unit.GetComponent<Icontype>().type == iconPrefab.GetComponent<Icontype>().type && deleted == false)
                {
                    GameObject deleteIcon = iconBar.mapGrid[x, y].unit;
                    iconBar.mapGrid[x, y].unit = null;
                    Destroy(deleteIcon);
                    deleted = true;
                    emptyX = x;
                    emptyY = y;
                }
                else if (iconBar.mapGrid[x, y].unit != null && deleted == true)
                {
                    GameObject currentUnit = iconBar.mapGrid[x, y].unit;
                    iconBar.mapGrid[emptyX, emptyY].unit = currentUnit;
                    iconBar.mapGrid[x, y].unit = null;
                    currentUnit.transform.position = iconBar.mapGrid[emptyX, emptyY].centerPosition;
                    emptyX = x;
                    emptyY = y;
                }
            }
        }
    }
}
