using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TypeAndClassHandler : MonoBehaviour
{
    public enum unitType { none, aggressive, amphibian, ancient, diggers, fire, fly, ice, magic, nightactive, poisenous, queen, rainbow, shell, shellbreakers, slow, sound }

    List<TypeStruct> typeStructs;

    public class TypeStruct
    {
        public TypeStruct(unitType thisType, List<int> thisThreshhold, int thisCounter)
        {
            type = thisType;
            threshhold = thisThreshhold;
            counter = thisCounter;
        }
        private unitType type;
        private List<int> threshhold;
        private int counter;

        public unitType GetStructType()
        {
            return type;
        }

        public void AddToThreshHold(int amount)
        {
            threshhold.Add(amount);
        }

        public void SetThreshHold(List<int> newThreshhold)
        {
            threshhold = newThreshhold;
        }

        public List<int> GetThreshhold()
        {
            return threshhold;
        }

        public void ResetCounter()
        {
            counter = 0;
        }

        public void AddtoCount(int amount)
        {
            counter += amount;
        }

        public int GetCounter ()
        {
            return counter;
        }

    }

    TypeAndClassEventHandler typeAndClassEventHandler;
    // Start is called before the first frame update
    void Start()
    {
        typeAndClassEventHandler = TypeAndClassEventHandler.instance;
        typeAndClassEventHandler.createTeam += CreateTeamComb;
        typeAndClassEventHandler.SetTypeAndClass += AddTypeAndClass;
        typeAndClassEventHandler.RemoveTypeAndClass += RemoveTypeAndClass;
        typeStructs = new List<TypeStruct>();
        InitTypeStruct();
        ResetAllCounter();
        CreateAllThreshholds();
    }

    private void InitTypeStruct()
    {
        foreach (unitType type in Enum.GetValues(typeof(unitType)))
        {
            TypeStruct tempTypeStruct = new TypeStruct(type, new List<int>(), 0);
            typeStructs.Add(tempTypeStruct);
        }
    } 

    private void AddTypeAndClass(object sender, TypeAndClassEventHandler.UnitTypeAndClass args)
    {
        if (args.GetTeamName() == this.name)
        {
            UpdateCountTypes(args.GetFirstType(), true);
            UpdateCountTypes(args.GetSecondType(), true);
            UpdateCountTypes(args.GetThirdType(), true);
        }
    }

    private void RemoveTypeAndClass(object sender, TypeAndClassEventHandler.UnitTypeAndClass args)
    {
        if (args.GetTeamName() == this.name) 
        {
            UpdateCountTypes(args.GetFirstType(), false);
            UpdateCountTypes(args.GetSecondType(), false);
            UpdateCountTypes(args.GetThirdType(), false);
        }
    }

    private void UpdateCountTypes(unitType newEntry, bool add)
    {
        int amount;
        if (add)
        {
            amount = 1;
        }
        else
        {
            amount = -1;
        }

        foreach (TypeStruct typeStruct in typeStructs)
        {
            if (newEntry == typeStruct.GetStructType() && newEntry != unitType.none)
            {
                typeStruct.AddtoCount(amount);
                int count = 0;
                foreach (int threshhold in typeStruct.GetThreshhold())
                {
                    if (typeStruct.GetCounter() >= threshhold)
                    {
                        count = threshhold;
                    }
                    else
                    {
                        break;
                    }
                }
                typeAndClassEventHandler.SendIconUpdate(typeStruct.GetStructType(), this.name, count);
            }
        }
    }

    public void CreateTeamComb(object sender, EventArgs args)
    {
        foreach (TypeStruct typeStruct in typeStructs)
        {
            int amount = 0;
            foreach (int threshhold in typeStruct.GetThreshhold())
            {
                if (typeStruct.GetCounter() >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            if (amount > 0)
            {
                typeAndClassEventHandler.SendTypeBuff(typeStruct.GetStructType(), this.name, amount);
            }
        }
    }

    private void ResetAllCounter()
    {
        foreach (TypeStruct typeStruct in typeStructs)
        {
            typeStruct.ResetCounter();
        }
}

    // threshholds

    private void CreateAllThreshholds()
    {

        foreach (TypeStruct typeStruct in typeStructs)
        {
            switch (typeStruct.GetStructType())
            {
                case unitType.aggressive:
                    typeStruct.SetThreshHold(CreateAggressiveThresholds());
                    break;
                case unitType.amphibian:
                    typeStruct.SetThreshHold(CreateAmphibianThresholds());
                    break;
                case unitType.ancient:
                    typeStruct.SetThreshHold(CreateAncientThresholds());
                    break;
                case unitType.diggers:
                    typeStruct.SetThreshHold(CreateDiggersThresholds());
                    break;
                case unitType.fire:
                    typeStruct.SetThreshHold(CreateFireThresholds());
                    break;
                case unitType.fly:
                    typeStruct.SetThreshHold(CreateFlyThresholds());
                    break;
                case unitType.ice:
                    typeStruct.SetThreshHold(CreateIceThresholds());
                    break;
                case unitType.magic:
                    typeStruct.SetThreshHold(CreateMagicThresholds());
                    break;
                case unitType.nightactive:
                    typeStruct.SetThreshHold(CreateNightactiveThresholds());
                    break;
                case unitType.poisenous:
                    typeStruct.SetThreshHold(CreatePoisenousThresholds());
                    break;
                case unitType.queen:
                    typeStruct.SetThreshHold(CreateQueenThresholds());
                    break;
                case unitType.rainbow:
                    typeStruct.SetThreshHold(CreateRainbowThresholds());
                    break;
                case unitType.shell:
                    typeStruct.SetThreshHold(CreateShellThresholds());
                    break;
                case unitType.shellbreakers:
                    typeStruct.SetThreshHold(CreateShellbreakersThresholds());
                    break;
                case unitType.slow:
                    typeStruct.SetThreshHold(CreateSlowThresholds());
                    break;
                case unitType.sound:
                    typeStruct.SetThreshHold(CreateSoundThresholds());
                    break;
            }
        }
    }

    private List<int> CreateAggressiveThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateAmphibianThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateAncientThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateDiggersThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateFireThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateFlyThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateIceThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateMagicThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateNightactiveThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreatePoisenousThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateQueenThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateRainbowThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateShellThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateShellbreakersThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateSlowThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }

    private List<int> CreateSoundThresholds()
    {
        List<int> threshHold = new List<int>();
        threshHold.Add(2);
        return threshHold;
    }
}
