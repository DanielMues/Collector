using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialMove : MonoBehaviour
{
    [Header("Settings")]
    public int chooseAction;
    public int shieldAmount = 10;
    public float stunAmount = 1f;
    public int damageOverTime = 1;
    public float timeDamageOverTime = 3f;
    public float timeDamageSteps = 0.5f;
    public int lifeStealDamage = 15;
    public float lifeStealconversion = 0.5f;

    private bool startDamageOverTime = false;
    private float startTime;
    private float currentTime;
    GameObject damageEnemy;

    public void doAction(GameObject enemy)
    {
        switch (chooseAction)
        {
            case 1:
                AddShield();
                break;
            case 2:
                StunEnemy(enemy);
                break;
            case 3:
                StartDamageOverTime(enemy);
                break;
            case 4:
                LifeSteal(enemy);
                break;
        }

    }

    private void Update()
    {
        if (startDamageOverTime)
        {
            DamageOverTime();
        }
    }

    private void AddShield()
    {
        this.GetComponent<UnitStats>().SetShield(shieldAmount);
    }

    private void StunEnemy(GameObject enemy)
    {
        enemy.GetComponent<UnitAction>().addTimeTillNextMove(stunAmount);
    }

    private void StartDamageOverTime(GameObject enemy)
    {
        startDamageOverTime = true;
        currentTime = Time.deltaTime;
        startTime = currentTime;
        damageEnemy = enemy;
    }
    // probleme - to do synchron???
    private void DamageOverTime()
    {
        int multiplier = 1;
        if(startTime + (multiplier*timeDamageSteps) <= currentTime && currentTime <= startTime + timeDamageOverTime && damageEnemy != null)
        {
            damageEnemy.GetComponent<UnitStats>().TakeDamage(damageOverTime);
        }
        else if(currentTime > startTime + timeDamageOverTime)
        {
            startDamageOverTime = false;
        }
        currentTime += Time.deltaTime;
    }

    private void PushEnemyBack(GameObject enemy)
    {

    }

    private void LifeSteal(GameObject enemy)
    {
        enemy.GetComponent<UnitStats>().TakeDamage(lifeStealDamage);
        float heal = lifeStealDamage * lifeStealconversion;
        this.GetComponent<UnitStats>().addHealth(heal);
    }

    private void DebuffEnemy(GameObject enemy)
    {

    }
}
