using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TypeAndClassHandler : MonoBehaviour
{
    public enum unitTyp { masked, spicked, hardened }
    public enum unitClass { bird, turtle }

    private List<int> threshHoldBird;
    private List<int> threshHoldTurtle;
    private List<int> threshHoldMasked;
    private List<int> threshHoldSpicked;
    private List<int> threshHoldHardened;

    // class counter
    private int counterBird;
    private int counterTurtle;

    // type counter
    private int counterMasked;
    private int counterSpicked;
    private int counterHardened;

    List<unitClass> teamClasses;
    List<unitTyp> teamTypes;
    TypeAndClassEventHandler typeAndClassEventHandler;
    // Start is called before the first frame update
    void Start()
    {
        typeAndClassEventHandler = TypeAndClassEventHandler.instance;
        typeAndClassEventHandler.createTeam += CreateTeamComb;
        typeAndClassEventHandler.SetTypeAndClass += AddTypeAndClass;
        typeAndClassEventHandler.RemoveTypeAndClass += RemoveTypeAndClass;
        teamClasses = new List<unitClass>();
        teamTypes = new List<unitTyp>();
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
            teamClasses.Add(args.GetClass());
            teamTypes.Add(args.GetType());
        }
    }

    private void RemoveTypeAndClass(object sender, TypeAndClassEventHandler.UnitTypeAndClass args)
    {
        if (args.GetTeamName() == this.name) 
        {
            teamClasses.Remove(args.GetClass());
            teamTypes.Remove(args.GetType());
        }
    }

    private void CountClasses()
    {
        foreach(unitClass class_ in teamClasses)
        {
            switch (class_)
            {
                case unitClass.bird:
                    counterBird += 1;
                    break;
                case unitClass.turtle:
                    counterTurtle += 1;
                    break;
            }
        }
    }

    private void CountTypes()
    {
        foreach (unitTyp type in teamTypes)
        {
            switch (type)
            {
                case unitTyp.masked:
                    counterMasked += 1;
                    break;
                case unitTyp.spicked:
                    counterSpicked += 1;
                    break;
                case unitTyp.hardened:
                    counterHardened += 1;
                    break;
            }
        }
    }

    public void CreateTeamComb(object sender, EventArgs args)
    {
        CountClasses();
        CountTypes();
        // class
        if(counterBird > 0)
        {
            int amount = 0;
            foreach(int threshhold in threshHoldBird)
            {
                if(counterBird >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.sendBirdBuff(amount);
        }
        if(counterTurtle > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldTurtle)
            {
                if (counterTurtle >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.sendTurtleBuff(amount);
        }
        //type
        if(counterMasked > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldMasked)
            {
                if (counterMasked >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.sendMaskedBuff(amount);
        }
        if(counterSpicked > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldSpicked)
            {
                if (counterSpicked >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.sendBirdBuff(amount);
        }
        if(counterHardened > 0)
        {
            int amount = 0;
            foreach (int threshhold in threshHoldHardened)
            {
                if (counterHardened >= threshhold)
                {
                    amount = threshhold;
                }
                else
                {
                    break;
                }
            }
            typeAndClassEventHandler.sendHardenedBuff(amount);
        }
    }

    private void ResetAllCounter()
    {
        counterBird = 0;
        counterTurtle = 0;
        counterMasked = 0;
        counterSpicked = 0;
        counterHardened = 0;
    }

    private void CreateAllThreshholds()
    {
        //classes
        CreateBirdThresholds();
        CreateTurtleThresholds();
        //types
        CreateMaskedThresholds();
        CreateHardenedThresholds();
        CreateSpickedThresholds();
    }

    // class threshholds
    private void CreateBirdThresholds()
    {
        threshHoldBird = new List<int>();
        threshHoldBird.Add(2);
        threshHoldBird.Add(4);
        threshHoldBird.Add(6);
    }

    private void CreateTurtleThresholds()
    {
        threshHoldTurtle = new List<int>();
        threshHoldTurtle.Add(2);
        threshHoldTurtle.Add(4);
        threshHoldTurtle.Add(6);
    }

    // type threshholds

    private void CreateMaskedThresholds()
    {
        threshHoldMasked = new List<int>();
        threshHoldMasked.Add(2);
        threshHoldMasked.Add(4);
        threshHoldMasked.Add(6);
    }

    private void CreateSpickedThresholds()
    {
        threshHoldSpicked = new List<int>();
        threshHoldSpicked.Add(2);
        threshHoldSpicked.Add(4);
        threshHoldSpicked.Add(6);
    }

    private void CreateHardenedThresholds()
    {
        threshHoldHardened = new List<int>();
        threshHoldHardened.Add(2);
        threshHoldHardened.Add(4);
        threshHoldHardened.Add(6);
    }
}
