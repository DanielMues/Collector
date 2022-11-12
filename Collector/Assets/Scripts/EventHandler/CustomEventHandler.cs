using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomEventHandler : MonoBehaviour
{
    public static CustomEventHandler instance;
    private void Start()
    {
        instance = this;
    }
    public class UnitInformation : EventArgs
    {
        public GameObject unit;
        public Vector3 worldPosition;
    }

    // custom Spawn Turret Event
    public event EventHandler<UnitInformation> PlaceUnitOnField;

    public void SendPlaceUnit(GameObject unit, Vector3 worldPosition)
    {
        PlaceUnitOnField?.Invoke(this, new UnitInformation { unit = unit, worldPosition = worldPosition});
    }

    public event EventHandler<UnitInformation> DeleteUnitFromField;

    public void DeleteUnitOnField(GameObject unit, Vector3 worldPosition)
    {
        DeleteUnitFromField?.Invoke(this, new UnitInformation { unit = unit, worldPosition = worldPosition});
    }

    public event EventHandler<UnitInformation> UnitGotClicked;

    public void UnitClicked(GameObject unit, Vector3 worldPosition)
    {
        UnitGotClicked?.Invoke(this, new UnitInformation { unit = unit, worldPosition = worldPosition });
    }

    public event EventHandler<UnitInformation> SwapUnit;

    public void SwapSelectedUnit(GameObject unit, Vector3 worldPosition)
    {
        SwapUnit?.Invoke(this, new UnitInformation { unit = unit, worldPosition = worldPosition });
    }
}
