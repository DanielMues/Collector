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

    public class UnitList : EventArgs
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

    public class UnitMovement : EventArgs
    {
        private bool y_;
        private bool up_;
        private GameObject unit_;
        public UnitMovement(bool y, bool up, GameObject unit)
        {
            y_ = y;
            up_ = up;
            unit_ = unit;
        }

        public bool GetY()
        {
            return y_;
        }

        public bool GetUp()
        {
            return up_;
        }

        public GameObject GetUnit()
        {
            return unit_;
        }
    }

    public event EventHandler<UnitMovement> MoveUnit;

    public void MoveTheUnit(bool y, bool up, GameObject unit)
    {
        MoveUnit?.Invoke(this, new UnitMovement(y, up, unit));
    }
}
