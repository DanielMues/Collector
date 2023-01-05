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
    
    public event EventHandler<EventArgs> createTeam;

    public void createTeams()
    {
        createTeam?.Invoke(this, new EventArgs());
    }

    public class TypeBuffInformation : EventArgs
    {
        private TypeAndClassHandler.unitType type_;
        private string team_;
        private int amount_;

        public TypeBuffInformation(TypeAndClassHandler.unitType type, string team, int amount)
        {
            type_ = type;
            team_ = team;
            amount_ = amount;
        }

        public TypeAndClassHandler.unitType GetTypeType()
        {
            return type_;
        }

        public String GetTeam()
        {
            return team_;
        }

        public int GetAmount()
        {
            return amount_;
        }
    }

    public event EventHandler<TypeBuffInformation> sendTypeBuff;
    public event EventHandler<TypeBuffInformation> sendIconUpdate;

    public void SendTypeBuff(TypeAndClassHandler.unitType type, string team, int amount) { sendTypeBuff?.Invoke(this, new TypeBuffInformation(type, team, amount)); }
    public void SendIconUpdate(TypeAndClassHandler.unitType type, string team, int amount) { sendIconUpdate?.Invoke(this, new TypeBuffInformation(type, team, amount)); }
}
