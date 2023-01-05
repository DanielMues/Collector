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
        private bool y_; // x or y direction
        private bool up_; // + or - in the direction
        private GameObject unit_; // unit to move
        private int steps_; // amount of steps 
        private bool specialMove_; // if called by a special move
        public UnitMovement(bool y, bool up, GameObject unit, int steps, bool specialMove)
        {
            y_ = y;
            up_ = up;
            unit_ = unit;
            steps_ = steps;
            specialMove_ = specialMove;
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
        
        public int GetSteps()
        {
            return steps_;
        }

        public bool GetIfSpecialMove()
        {
            return specialMove_;
        }
    }

    public event EventHandler<UnitMovement> MoveUnit;

    public void MoveTheUnit(bool y, bool up, GameObject unit, int steps, bool specialMove)
    {
        MoveUnit?.Invoke(this, new UnitMovement(y, up, unit, steps, specialMove));
    }
}
