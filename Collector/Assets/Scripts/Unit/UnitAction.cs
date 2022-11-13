using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitAction : MonoBehaviour
{
    private FightEventHandler fightEventHandler;
    private List<GameObject> unitList;

    private bool startFight;
    private float timeBetweenAttacks;
    private float restTimeForAttack;
    private GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        fightEventHandler = FightEventHandler.instance;
        fightEventHandler.SendStartFight += StartFight;
        currentEnemy = null;
        unitList = null;
        startFight = false;
        timeBetweenAttacks = this.GetComponent<UnitStats>().GetSpeed();
        restTimeForAttack = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFight)
        {
            if (currentEnemy == null && unitList != null)
            {
                currentEnemy = FindEnemyInRange(this.GetComponent<UnitStats>().GetRange(), this.transform.position, unitList);
            }
            else if (currentEnemy != null && restTimeForAttack <= 0)
            {
                currentEnemy.GetComponent<UnitStats>().TakeDamage(this.GetComponent<UnitStats>().GetAttackDamage());
            }
            restTimeForAttack -= Time.deltaTime;
        }
    }

    private void UpdateUnitList(object sender, FightEventHandler.UnitList args)
    {
        unitList = args.allUnits;
        Debug.Log("list updated");
    }

    private void StartFight(object sender, EventArgs args)
    {
        startFight = true;
    }

    private GameObject FindEnemyInRange(int Range, Vector3 position, List<GameObject> units)
    {
        GameObject enemy = null;
        float shortestDistance = Range;
        foreach (GameObject unit in units)
        {
            float distance = (position - unit.transform.position).magnitude;
            if (distance < Range && distance != 0 && distance < shortestDistance)
            {
                enemy = unit;
                shortestDistance = distance;
            }
        }
        return enemy;
    }

    private GameObject FindClosestEnemy(Vector3 position, List<GameObject> units)
    {
        GameObject enemy = null;
        float shortestDistance = 0;
        foreach (GameObject unit in units)
        {
            float distance = (position - unit.transform.position).magnitude;
            if (shortestDistance == 0)
            {
                shortestDistance = distance;
                enemy = unit;
            }
            else if (distance < shortestDistance && distance != 0)
            {
                shortestDistance = distance;
                enemy = unit;
            }
        }
        return enemy;
    }

}
