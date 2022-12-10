using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TypeAndClassEventHandler : MonoBehaviour
{
    public static TypeAndClassEventHandler instance;

    private void Start()
    {
        instance = this;
    }

    public class UnitTypeAndClass: EventArgs
    {
        private TypeAndClassHandler.unitClass unitClass;
        private TypeAndClassHandler.unitTyp unitType;
        private string teamName;

        public UnitTypeAndClass(TypeAndClassHandler.unitTyp thisType, TypeAndClassHandler.unitClass thisClass, string team)
        {
            unitClass = thisClass;
            unitType = thisType;
            teamName = team;
        }

        public TypeAndClassHandler.unitClass GetClass()
        {
            return unitClass;
        }

        public TypeAndClassHandler.unitTyp GetType()
        {
            return unitType;
        }

        public string GetTeamName()
        {
            return teamName;
        }
    }

    public event EventHandler<UnitTypeAndClass> SetTypeAndClass;

    public void setUnitTypeAndClass(TypeAndClassHandler.unitClass myClass, TypeAndClassHandler.unitTyp myType, string team)
    {
        SetTypeAndClass?.Invoke(this, new UnitTypeAndClass(myType, myClass, team));
    }

    public event EventHandler<UnitTypeAndClass> RemoveTypeAndClass;

    public void deleteUnitTypeAndClass(TypeAndClassHandler.unitClass myClass, TypeAndClassHandler.unitTyp myType, string team)
    {
        RemoveTypeAndClass?.Invoke(this, new UnitTypeAndClass(myType, myClass, team));
    }
    
    public class AmountOfUnits: EventArgs
    {
        private int amount_;

        public AmountOfUnits(int amount)
        {
            amount_ = amount;
        }

        public int GetAmount()
        {
            return amount_;
        }
    }

    public event EventHandler<AmountOfUnits> birdBuff;

    public void sendBirdBuff(int amount)
    {
        birdBuff?.Invoke(this, new AmountOfUnits(amount));
    }

    public event EventHandler<AmountOfUnits> turtleBuff;

    public void sendTurtleBuff(int amount)
    {
        turtleBuff?.Invoke(this, new AmountOfUnits(amount));
    }

    public event EventHandler<AmountOfUnits> maskedBuff;

    public void sendMaskedBuff(int amount)
    {
        maskedBuff?.Invoke(this, new AmountOfUnits(amount));
    }

    public event EventHandler<AmountOfUnits> spickedBuff;

    public void sendSpickedBuff(int amount)
    {
        spickedBuff?.Invoke(this, new AmountOfUnits(amount));
    }

    public event EventHandler<AmountOfUnits> hardenedBuff;

    public void sendHardenedBuff(int amount)
    {
        hardenedBuff?.Invoke(this, new AmountOfUnits(amount));
    }

    public event EventHandler<EventArgs> createTeam;

    public void createTeams()
    {
        createTeam?.Invoke(this, new EventArgs());
    }
}
