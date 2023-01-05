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
    private bool goToYFirst;

    private UnitStats myUnitStats;
    // Start is called before the first frame update
    void Start()
    {
        fightEventHandler = FightEventHandler.instance;
        fightEventHandler.SendStartFight += StartFight;
        fightEventHandler.SendAllUnits += UpdateUnitList;
        currentEnemy = null;
        unitList = null;
        startFight = false;
        myUnitStats = this.GetComponent<UnitStats>();
        timeBetweenAttacks = myUnitStats.GetAttackSpeed();
        timeBetweenMoving = myUnitStats.GetMoveSpeed();
        restTimeTillMove = timeBetweenAttacks;
        goToYFirst = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFight && unitList != null && restTimeTillMove <= 0)
        {
            currentEnemy = FindEnemyInRange(myUnitStats.GetRange(), this.transform.position, unitList);

            if (currentEnemy == null)
            {
                currentEnemy = FindClosestEnemy(this.transform.position, unitList);
                if (currentEnemy != null)
                {
                    MoveToEnemy(currentEnemy);
                    restTimeTillMove = timeBetweenMoving;
                    currentEnemy = null;
                }
            }
            else if (currentEnemy != null && myUnitStats.GetMana() >= myUnitStats.GetMaxMana())
            {
                this.GetComponent<TestSpecialMove>().doAction(currentEnemy);
                myUnitStats.ResetMana();
                restTimeTillMove = timeBetweenAttacks;
            }
            else if (currentEnemy != null)
            {
                currentEnemy.GetComponent<UnitStats>().TakeDamage(myUnitStats.GetAttackDamage());
                restTimeTillMove = timeBetweenAttacks;
                myUnitStats.AddMana(myUnitStats.GetManaProAttack());
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
            if(unit != null && myUnitStats.GetTeam() != unit.GetComponent<UnitStats>().GetTeam()) // prevents to access a dead unit
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
            if(unit != null && myUnitStats.GetTeam() != unit.GetComponent<UnitStats>().GetTeam()) // prevents to access a dead unit
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
        if (goToYFirst)
        {
            float yDifference = this.transform.position.y - enemy.transform.position.y;
            if (yDifference > 0)
            {
                if (!CheckIfTeamMateIsInYourWay(1))
                {
                    MoveUnitWrapperFunction(1);
                }
                else
                {
                    MoveUnitWrapperFunction(3);
                }
            }
            else if (yDifference < 0)
            {
                if (!CheckIfTeamMateIsInYourWay(2))
                {
                    MoveUnitWrapperFunction(2);
                }
                else
                {
                    MoveUnitWrapperFunction(4);
                }
            }
            else if (yDifference == 0)
            {
                float xDifference = this.transform.position.x - enemy.transform.position.x;
                if (xDifference > 0)
                {
                    if (!CheckIfTeamMateIsInYourWay(3))
                    {
                        MoveUnitWrapperFunction(3);
                    }
                    else
                    {
                        MoveUnitWrapperFunction(1);
                        goToYFirst = false;
                    }

                }
                else
                {
                    if (!CheckIfTeamMateIsInYourWay(4))
                    {
                        MoveUnitWrapperFunction(4);
                    }
                    else
                    {
                        MoveUnitWrapperFunction(2);
                        goToYFirst = false;
                    }
                }
            }
        }
        else
        {
            float xDifference = this.transform.position.x - enemy.transform.position.x;
            if (xDifference > 0)
            {
                if (!CheckIfTeamMateIsInYourWay(3))
                {
                    MoveUnitWrapperFunction(3);
                }
                else
                {
                    MoveUnitWrapperFunction(1);
                }
                
            }
            else if (xDifference < 0)
            {
                if (!CheckIfTeamMateIsInYourWay(4))
                {
                    MoveUnitWrapperFunction(4);
                }
                else
                {
                    MoveUnitWrapperFunction(2);
                }
            }
            else if (xDifference == 0)
            {
                float yDifference = this.transform.position.y - enemy.transform.position.y;
                if (yDifference > 0)
                {
                    if (!CheckIfTeamMateIsInYourWay(1))
                    {
                        MoveUnitWrapperFunction(1);
                    }
                    else
                    {
                        MoveUnitWrapperFunction(3);
                        goToYFirst = true;
                    }
                }
                else
                {
                    if (!CheckIfTeamMateIsInYourWay(2))
                    {
                        MoveUnitWrapperFunction(2);
                    }
                    else
                    {
                        MoveUnitWrapperFunction(4);
                        goToYFirst = true;
                    }
                }
            }
        }
    }

    public void AddTimeTillNextMove(float time)
    {
        restTimeTillMove += time;
    }

    public bool CheckIfTeamMateIsInYourWay(long direction)
    {
        foreach (GameObject unit in unitList)
        {
            if(unit.GetComponent<UnitStats>().GetTeam() == this.GetComponent<UnitStats>().GetTeam() && unit != this.gameObject)
            {
                switch (direction)
                {
                    case 1:
                        return unit.transform.position == new Vector3(this.transform.position.x, this.transform.position.y - 2, this.transform.position.z);
                    case 2:
                        return unit.transform.position == new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z);
                    case 3:
                        return unit.transform.position == new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z);
                    case 4:
                        return unit.transform.position == new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z);
                }
            }
        }
        return false;
    }

    public void MoveUnitWrapperFunction(long direction)
    {
        switch (direction)
        {
            case 1:
                fightEventHandler.MoveTheUnit(true, false, this.gameObject, 1, false);
                break;
            case 2:
                fightEventHandler.MoveTheUnit(true, true, this.gameObject, 1, false);
                break;
            case 3:
                fightEventHandler.MoveTheUnit(false, false, this.gameObject, 1, false);
                break;
            case 4:
                fightEventHandler.MoveTheUnit(false, true, this.gameObject, 1, false);
                break;
        }
    }
}
