using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FightEventHandler : MonoBehaviour
{
    public static FightEventHandler instance;
    private void Start()
    {
        instance = this;
    }

    public class UnitList: EventArgs
    {
        public List<GameObject> allUnits;
    }

    public event EventHandler<UnitList> SendAllUnits;

    public void UpdateUnitList(List<GameObject> unitList)
    {
        SendAllUnits?.Invoke(this, new UnitList { allUnits = unitList });
    }

    public event EventHandler<EventArgs> SendStartFight;

    public void StartTheFight()
    {
        SendStartFight?.Invoke(this, new EventArgs());
    }
}
