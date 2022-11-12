using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitAction : MonoBehaviour
{
    private FightEventHandler fightEventHandler;
    private List<GameObject> unitList;
    // Start is called before the first frame update
    void Start()
    {
        fightEventHandler = FightEventHandler.instance;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateUnitList(object sender, FightEventHandler.UnitList args)
    {
        unitList = args.allUnits;
    }

}
