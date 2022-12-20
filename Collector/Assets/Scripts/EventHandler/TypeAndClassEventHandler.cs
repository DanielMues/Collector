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
        private TypeAndClassHandler.unitType firstUnitType;
        private TypeAndClassHandler.unitType secondUnitType;
        private TypeAndClassHandler.unitType thirdUnitType;
        private string teamName;

        public UnitTypeAndClass(TypeAndClassHandler.unitType thisFirstType, TypeAndClassHandler.unitType thisSecondType, TypeAndClassHandler.unitType thisThirdType, string team)
        {
            firstUnitType = thisFirstType;
            secondUnitType = thisSecondType;
            thirdUnitType = thisThirdType;
            teamName = team;
        }

        public TypeAndClassHandler.unitType GetFirstType()
        {
            return firstUnitType;
        }

        public TypeAndClassHandler.unitType GetSecondType()
        {
            return secondUnitType;
        }

        public TypeAndClassHandler.unitType GetThirdType()
        {
            return thirdUnitType;
        }

        public string GetTeamName()
        {
            return teamName;
        }
    }

    public event EventHandler<UnitTypeAndClass> SetTypeAndClass;

    public void setUnitTypeAndClass(TypeAndClassHandler.unitType myFirstType, TypeAndClassHandler.unitType mySecondType, TypeAndClassHandler.unitType myThirdType, string team)
    {
        SetTypeAndClass?.Invoke(this, new UnitTypeAndClass(myFirstType, mySecondType, myThirdType, team));
    }

    public event EventHandler<UnitTypeAndClass> RemoveTypeAndClass;

    public void deleteUnitTypeAndClass(TypeAndClassHandler.unitType myFirstType, TypeAndClassHandler.unitType mySecondType, TypeAndClassHandler.unitType myThirdType, string team)
    {
        RemoveTypeAndClass?.Invoke(this, new UnitTypeAndClass(myFirstType, mySecondType, myThirdType, team));
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

    // to do change
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
