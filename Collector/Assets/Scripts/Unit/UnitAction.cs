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
    private float restTimeTillMove;
    private float timeBetweenMoving;
    private GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        fightEventHandler = FightEventHandler.instance;
        fightEventHandler.SendStartFight += StartFight;
        fightEventHandler.SendAllUnits += UpdateUnitList;
        currentEnemy = null;
        unitList = null;
        startFight = false;
        timeBetweenAttacks = this.GetComponent<UnitStats>().GetAttackSpeed();
        timeBetweenMoving = this.GetComponent<UnitStats>().GetMoveSpeed();
        restTimeTillMove = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFight && unitList != null && restTimeTillMove <= 0)
        {
            currentEnemy = FindEnemyInRange(this.GetComponent<UnitStats>().GetRange(), this.transform.position, unitList);
            
            if(currentEnemy == null)
            {
                currentEnemy = FindClosestEnemy(this.transform.position, unitList);
                if(currentEnemy != null)
                {
                    MoveToEnemy(currentEnemy);
                    restTimeTillMove = timeBetweenMoving;
                    currentEnemy = null;
                }
            }
            else if (currentEnemy != null)
            {
                currentEnemy.GetComponent<UnitStats>().TakeDamage(this.GetComponent<UnitStats>().GetAttackDamage());
                restTimeTillMove = timeBetweenAttacks;
            }
        }
        restTimeTillMove -= Time.deltaTime;
    }

    private void UpdateUnitList(object sender, FightEventHandler.UnitList args)
    {
        unitList = args.allUnits;
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
            if(unit != null && this.GetComponent<UnitStats>().GetTeam() != unit.GetComponent<UnitStats>().GetTeam()) // prevents to access a dead unit
            {
                float distance = (position - unit.transform.position).magnitude;
                if (distance <= Range && distance != 0 && distance <= shortestDistance)
                {
                    enemy = unit;
                    shortestDistance = distance;
                }
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
            if(unit != null && this.GetComponent<UnitStats>().GetTeam() != unit.GetComponent<UnitStats>().GetTeam()) // prevents to access a dead unit
            {
                float distance = (position - unit.transform.position).magnitude;
                if (shortestDistance == 0 && distance != 0)
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
        }
        return enemy;
    }

    private void MoveToEnemy(GameObject enemy)
    {
        float yDifference = this.transform.position.y - enemy.transform.position.y;
        if (yDifference > 0)
        {
            fightEventHandler.MoveTheUnit(true, false, this.gameObject);
        }
        else if(yDifference < 0)
        {
            fightEventHandler.MoveTheUnit(true, true, this.gameObject);
        }
        else if(yDifference == 0)
        {
            float xDifference = this.transform.position.x - enemy.transform.position.x;
            if(xDifference > 0)
            {
                fightEventHandler.MoveTheUnit(false, false, this.gameObject);
            }
            else
            {
                fightEventHandler.MoveTheUnit(false, true, this.gameObject);
            }
        }
    }

}
