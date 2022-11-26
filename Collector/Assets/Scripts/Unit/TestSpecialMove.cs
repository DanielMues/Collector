using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialMove : MonoBehaviour
{
    [Header("Settings")]
    //general
    public int chooseAction;
    private float currentTime;
    private float currentCounter;
    private GameObject damageEnemy;
    //shield
    public int shieldAmount = 10;
    //stun
    public float stunAmount = 1f;
    //lifesteal
    public int lifeStealDamage = 15;
    public float lifeStealconversion = 0.5f;
    // damage over time
    public int damageOverTime = 1;
    public float timeDamageOverTime = 3f;
    public float timeDamageSteps = 0.5f;
    private bool startDamageOverTime = false;
    private float startTime;
    // debuff
    public float debuffDamageDownScale = 0.2f;
    public float debuffTime = 3f;
    private bool startDebuffEnemy = false;
    private int oldDamage;
    

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
            case 5:
                StartDebuffDamage(enemy);
                break;
        }

    }

    private void Start()
    {
        currentTime = Time.time;
    }

    private void Update()
    {
        if (startDamageOverTime)
        {
            DamageOverTime();
        }
        else if (startDebuffEnemy)
        {
            DebuffEnemy();
        }
        currentTime += Time.deltaTime;
    }

    private void AddShield()
    {
        this.GetComponent<UnitStats>().SetShield(shieldAmount);
    }

    private void StunEnemy(GameObject enemy)
    {
        enemy.GetComponent<UnitAction>().AddTimeTillNextMove(stunAmount);
    }

    private void StartDamageOverTime(GameObject enemy)
    {
        startTime = currentTime;
        currentCounter = 0;
        damageEnemy = enemy;
        startDamageOverTime = true;
    }

    private void StartDebuffDamage(GameObject enemy)
    {
        damageEnemy = enemy;
        startDebuffEnemy = true;
        int enemyDamage = enemy.GetComponent<UnitStats>().GetAttackDamage();
        oldDamage = enemyDamage;
        float reducedDamage = enemyDamage * (1-debuffDamageDownScale);
        enemy.GetComponent<UnitStats>().setDamage((int)reducedDamage);
        currentCounter = debuffTime;
    }

    private void DamageOverTime()
    {

        if ( currentCounter <= 0 && currentTime <= startTime + timeDamageOverTime && damageEnemy != null)
        {
            damageEnemy.GetComponent<UnitStats>().TakeDamage(damageOverTime);
            currentCounter = timeDamageSteps;
        }
        else if(currentTime > startTime + timeDamageOverTime)
        {
            startDamageOverTime = false;
        }
        currentCounter -= Time.deltaTime;
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

    private void DebuffEnemy()
    {
        if(currentCounter <= 0)
        {
            damageEnemy.GetComponent<UnitStats>().setDamage(oldDamage);
            startDebuffEnemy = false;
        }
        currentCounter -= Time.deltaTime;
    }
}
