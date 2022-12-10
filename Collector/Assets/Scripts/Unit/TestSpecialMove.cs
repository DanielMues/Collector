using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialMove : MonoBehaviour
{
    [Header("Settings")]
    //general
    private float currentTime;
    private float currentCounter;
    private GameObject damageEnemy;
    [SerializeField]
    SpecialMove specialMove;
    enum SpecialMove { shield, stun, damageOverTime, lifeSteal, debuffDamage, pushBack }
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
    // push unit 
    private FightEventHandler fightEventHandler;

    public void doAction(GameObject enemy)
    {
        switch (specialMove)
        {
            case SpecialMove.shield:
                AddShield();
                break;
            case SpecialMove.stun:
                StunEnemy(enemy);
                break;
            case SpecialMove.damageOverTime:
                StartDamageOverTime(enemy);
                break;
            case SpecialMove.lifeSteal:
                LifeSteal(enemy);
                break;
            case SpecialMove.debuffDamage:
                StartDebuffDamage(enemy);
                break;
            case SpecialMove.pushBack:
                PushEnemyBack(enemy);
                break;
        }
    }

    private void Start()
    {
        currentTime = Time.time;
        fightEventHandler = FightEventHandler.instance;
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
        float yDifference = this.transform.position.y - enemy.transform.position.y;
        if (yDifference > 0)
        {
            fightEventHandler.MoveTheUnit(true, false, enemy, 2, true);
        }
        else if (yDifference < 0)
        {
            fightEventHandler.MoveTheUnit(true, true, enemy, 2, true);
        }
        else if (yDifference == 0)
        {
            float xDifference = this.transform.position.x - enemy.transform.position.x;
            if (xDifference > 0)
            {

                fightEventHandler.MoveTheUnit(false, false, enemy, 2, true);
            }
            else
            {
                fightEventHandler.MoveTheUnit(false, true, enemy, 2, true);
            }
        }
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
