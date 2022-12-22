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
    public event EventHandler<AmountOfUnits> aggressiveBuff;
    public event EventHandler<AmountOfUnits> amphibianBuff;
    public event EventHandler<AmountOfUnits> ancientBuff;
    public event EventHandler<AmountOfUnits> diggersBuff;
    public event EventHandler<AmountOfUnits> fireBuff;
    public event EventHandler<AmountOfUnits> flyBuff;
    public event EventHandler<AmountOfUnits> iceBuff;
    public event EventHandler<AmountOfUnits> magicBuff;
    public event EventHandler<AmountOfUnits> nightactiveBuff;
    public event EventHandler<AmountOfUnits> poisenousBuff;
    public event EventHandler<AmountOfUnits> queenBuff;
    public event EventHandler<AmountOfUnits> rainbowBuff;
    public event EventHandler<AmountOfUnits> shellBuff;
    public event EventHandler<AmountOfUnits> shellBreakerBuff;
    public event EventHandler<AmountOfUnits> slowBuff;
    public event EventHandler<AmountOfUnits> soundBuff;

    public void SendAggressiveBuff(int amount) { aggressiveBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendAmphibianBuff(int amount) { amphibianBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendAncientBuff(int amount) { ancientBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendDiggersBuff(int amount) { diggersBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendFireBuff(int amount) { fireBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendFlyBuff(int amount) { flyBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendIceBuff(int amount) { iceBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendMagicBuff(int amount) { magicBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendNightActiveBuff(int amount) { nightactiveBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendPoisenousBuff(int amount) { poisenousBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendQueenBuff(int amount) { queenBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendRainbowBuff(int amount) { rainbowBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendShellBuff(int amount) { shellBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendShellBreakerBuff(int amount) { shellBreakerBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendSlowBuff(int amount) { slowBuff?.Invoke(this, new AmountOfUnits(amount)); }
    public void SendSoundBuff(int amount) { soundBuff?.Invoke(this, new AmountOfUnits(amount)); }

    public event EventHandler<EventArgs> createTeam;

    public void createTeams()
    {
        createTeam?.Invoke(this, new EventArgs());
    }
}
