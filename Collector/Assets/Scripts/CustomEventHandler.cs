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

    // custom Spawn Turret Event
    public event EventHandler<PlaceUnit> PlaceUnitOnField;
    public class PlaceUnit : EventArgs
    {
        public GameObject unit;
        public Vector3 worldPosition;
    }

    public void SendPlaceUnit(GameObject unit, Vector3 worldPosition)
    {
        PlaceUnitOnField?.Invoke(this, new PlaceUnit { unit = unit, worldPosition = worldPosition});
    }
}
