using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TypeAndClassHandler : MonoBehaviour
{
    public enum unitType { none, aggressive, amphibian, ancient, diggers, fire, fly, ice, magic, nightactive, poisenous, queen, rainbow, shell, shellbreakers, slow, sound }

    private List<int> threshHoldAggressive;
    private List<int> threshHoldAmphibian;
    private List<int> threshHoldAncient;
    private List<int> threshHoldDiggers;
    private List<int> threshHoldFire;
    private List<int> threshHoldFly;
    private List<int> threshHoldIce;
    private List<int> threshHoldMagic;
    private List<int> threshHoldNightactive;
    private List<int> threshHoldPoisenous;
    private List<int> threshHoldQueen;
    private List<int> threshHoldRainbow;
    private List<int> threshHoldShell;
    private List<int> threshHoldShellbreakers;
    private List<int> threshHoldSlow;
    private List<int> threshHoldSound;


    private int counterAggresive;
    private int counterAmphibian;
    private int counterAncient;
    private int counterDiggers;
    private int counterFire;
    private int counterFly;
    private int counterIce;
    private int counterMagic;
    private int counterNightactive;
    private int counterPoisenous;
    private int counterQueen;
    private int counterRainbow;
    private int counterShell;
    private int counterShellbreakers;
    private int counterSlow;
    private int counterSound;

    

    List<unitType> teamTypes;
    TypeAndClassEventHandler typeAndClassEventHandler;
    // Start is called before the first frame update
    void Start()
    {
        typeAndClassEventHandler = TypeAndClassEventHandler.instance;
        typeAndClassEventHandler.createTeam += CreateTeamComb;
        typeAndClassEventHandler.SetTypeAndClass += AddTypeAndClass;
        typeAndClassEventHandler.RemoveTypeAndClass += RemoveTypeAndClass;
        teamTypes = new List<unitType>();
        ResetAllCounter();
        CreateAllThreshholds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddTypeAndClass(object sender, TypeAndClassEventHandler.UnitTypeAndClass args)
    {
        if (args.GetTeamName() == this.name)
        {
            teamTypes.Add(args.GetFirstType());
            teamTypes.Add(args.GetSecondType());
            teamTypes.Add(args.GetThirdType());
        }
    }

    private void RemoveTypeAndClass(object sender, TypeAndClassEventHandler.UnitTypeAndClass args)
    {
        if (args.GetTeamName() == this.name) 
        {
            teamTypes.Remove(args.GetFirstType());
            teamTypes.Remove(args.GetSecondType());
            teamTypes.Remove(args.GetThirdType());
        }
    }

    private void CountTypes()
    {
        foreach (unitType type in teamTypes)
        {
            switch (type)
            {
                case unitType.aggressive:
                    counterIce += 1;
                    break;
                case unitType.amphibian:
                    counterIce += 1;
                    break;
                case unitType.ancient:
                    counterIce += 1;
                    break;
                case unitType.diggers:
                    counterIce += 1;
                    break;
                case unitType.fire:
                    counterIce += 1;
                    break;
                case unitType.fly:
                    counterIce += 1;
                    break;
                case unitType.ice:
                    counterIce += 1;
                    break;
                case unitType.magic:
                    counterIce += 1;
                    break;
                case unitType.nightactive:
                    counterIce += 1;
                    break;
                case unitType.poisenous:
                    counterIce += 1;
                    break;
                case unitType.queen:
                    counterIce += 1;
                    break;
                case unitType.rainbow:
                    counterIce += 1;
                    break;
                case unitType.shell:
                    counterIce += 1;
                    break;
                case unitType.shellbreakers:
                    counterIce += 1;
                    break;
                case unitType.slow:
                    counterIce += 1;
                    break;
                case unitType.sound:
                    counterIce += 1;
                    break;
            }
        }
    }

    public void CreateTeamComb(object sender, EventArgs args)
    {
        //counting
        CountTypes();
        
        // buffsending
        if(counterAggresive > 0)
        {
            int amount = 0;
            foreach(int threshhold in threshHoldAggressive)
            {
                if(counterAggresive >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendAggressiveBuff(amount);
        }
        if (counterAmphibian > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldAmphibian)
            {
                if (counterAmphibian >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendAmphibianBuff(amount);
        }
        if (counterAncient > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldAncient)
            {
                if (counterAncient >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendAncientBuff(amount);
        }
        if (counterDiggers > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldDiggers)
            {
                if (counterDiggers >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendDiggersBuff(amount);
        }
        if (counterFire > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldFire)
            {
                if (counterFire >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendFireBuff(amount);
        }
        if (counterFly > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldFly)
            {
                if (counterFly >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendFlyBuff(amount);
        }
        if (counterIce > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldIce)
            {
                if (counterIce >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendIceBuff(amount);
        }
        if (counterMagic > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldMagic)
            {
                if (counterMagic >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendMagicBuff(amount);
        }
        if (counterNightactive > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldNightactive)
            {
                if (counterNightactive >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendNightActiveBuff(amount);
        }
        if (counterPoisenous > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldPoisenous)
            {
                if (counterPoisenous >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendPoisenousBuff(amount);
        }
        if (counterQueen > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldQueen)
            {
                if (counterQueen >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendQueenBuff(amount);
        }
        if (counterRainbow > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldRainbow)
            {
                if (counterRainbow >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendRainbowBuff(amount);
        }
        if (counterShell > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldShell)
            {
                if (counterShell >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendShellBuff(amount);
        }
        if (counterShellbreakers > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldShellbreakers)
            {
                if (counterShellbreakers >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendShellBreakerBuff(amount);
        }
        if (counterSlow > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldSlow)
            {
                if (counterSlow >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendSlowBuff(amount);
        }
        if (counterSound > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldSound)
            {
                if (counterSound >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.SendSoundBuff(amount);
        }
    }

    private void ResetAllCounter()
    {
        counterAggresive = 0;
        counterAmphibian = 0;
        counterAncient = 0;
        counterDiggers = 0;
        counterFire = 0;
        counterIce = 0;
        counterFly = 0;
        counterMagic = 0;
        counterNightactive = 0;
        counterPoisenous = 0;
        counterQueen = 0;
        counterRainbow = 0;
        counterShell = 0;
        counterShellbreakers = 0;
        counterSlow = 0;
        counterSound = 0;
}

    // threshholds

    private void CreateAllThreshholds()
    {
        CreateAggressiveThresholds();
        CreateAmphibianThresholds();
        CreateAncientThresholds();
        CreateDiggersThresholds();
        CreateFireThresholds();
        CreateFlyThresholds();
        CreateIceThresholds();
        CreateMagicThresholds();
        CreateNightactiveThresholds();
        CreatePoisenousThresholds();
        CreateQueenThresholds();
        CreateRainbowThresholds();
        CreateShellbreakersThresholds();
        CreateShellThresholds();
        CreateSlowThresholds();
        CreateSoundThresholds();
    }

    private void CreateAggressiveThresholds()
    {
        threshHoldAggressive = new List<int>();
        threshHoldAggressive.Add(2);
    }

    private void CreateAmphibianThresholds()
    {
        threshHoldAmphibian = new List<int>();
        threshHoldAmphibian.Add(2);
    }

    private void CreateAncientThresholds()
    {
        threshHoldAncient = new List<int>();
        threshHoldAncient.Add(2);
    }

    private void CreateDiggersThresholds()
    {
        threshHoldDiggers = new List<int>();
        threshHoldDiggers.Add(2);
    }

    private void CreateFireThresholds()
    {
        threshHoldFire = new List<int>();
        threshHoldFire.Add(2);
    }

    private void CreateFlyThresholds()
    {
        threshHoldFly = new List<int>();
        threshHoldFly.Add(2);
    }

    private void CreateIceThresholds()
    {
        threshHoldIce = new List<int>();
        threshHoldIce.Add(2);
    }

    private void CreateMagicThresholds()
    {
        threshHoldMagic = new List<int>();
        threshHoldMagic.Add(2);
    }

    private void CreateNightactiveThresholds()
    {
        threshHoldNightactive = new List<int>();
        threshHoldNightactive.Add(2);
    }

    private void CreatePoisenousThresholds()
    {
        threshHoldPoisenous = new List<int>();
        threshHoldPoisenous.Add(2);
    }

    private void CreateQueenThresholds()
    {
        threshHoldQueen = new List<int>();
        threshHoldQueen.Add(2);
    }

    private void CreateRainbowThresholds()
    {
        threshHoldRainbow = new List<int>();
        threshHoldRainbow.Add(2);
    }

    private void CreateShellThresholds()
    {
        threshHoldShell = new List<int>();
        threshHoldShell.Add(2);
    }

    private void CreateShellbreakersThresholds()
    {
        threshHoldShellbreakers = new List<int>();
        threshHoldShellbreakers.Add(2);
    }

    private void CreateSlowThresholds()
    {
        threshHoldSlow= new List<int>();
        threshHoldSlow.Add(2);
    }

    private void CreateSoundThresholds()
    {
        threshHoldSound = new List<int>();
        threshHoldSound.Add(2);
    }
}
