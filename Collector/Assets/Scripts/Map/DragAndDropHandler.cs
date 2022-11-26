using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropHandler : MonoBehaviour
{
    // Start is called before the first frame update
    CustomEventHandler customEventHandler;
    GameObject selectedUnit;
    Vector3 mousePositionOffset;
    void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.UnitGotClicked += UnitGotClicked;
        customEventHandler.SwapUnit += SwapUnits; 
        selectedUnit = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedUnit != null)
        {
            selectedUnit.transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UnitGotClicked(object sender, CustomEventHandler.UnitInformation args)
    {
        if (selectedUnit == null)
        {
            selectedUnit = args.unit;
            mousePositionOffset = selectedUnit.transform.position - GetMouseWorldPosition();
            customEventHandler.DeleteUnitOnField(args.unit, args.worldPosition);
        }
        else if (selectedUnit == args.unit)
        {
            selectedUnit = null;
            customEventHandler.SendPlaceUnit(args.unit, args.unit.transform.position);
        }
    }

    private void SwapUnits(object sender, CustomEventHandler.UnitInformation args)
    {
        selectedUnit = args.unit;
        mousePositionOffset = selectedUnit.transform.position - GetMouseWorldPosition();
    }
}
