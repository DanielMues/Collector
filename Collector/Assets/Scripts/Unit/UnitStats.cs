using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    // health
    [Header("Health")]
    [SerializeField]
    private int baseHealthPoints = 100;
    private int maxHealthPoints;
    private Transform healthBarHolder;
    private Transform healthBar;
    private int currentHealthPoints;

    // stuff
    [Header("Stuff")]
    [SerializeField]
    private float baseAttackSpeed = 1f;
    private float attackSpeed;
    [SerializeField]
    private float baseMoveSpeed = 1f;
    private float moveSpeed;
    [SerializeField]
    private int baseAttackDamage = 1;
    private int attackDamage;
    [SerializeField]
    private int baseRange = 1;
    private int range;
    [SerializeField]
    private string team = "A";

    // mana
    [Header("Mana")]
    [SerializeField]
    private int maxMana = 100;
    [SerializeField]
    private int manaProAttack = 10;
    [SerializeField]
    private int startMana = 0;
    private int currentMana;
    private Transform manaBarHolder;
    private Transform manaBar;

    // shield
    private int currentShield;
    private Transform shieldBarHolder;
    private Transform shieldBar;

    //types and classes
    [SerializeField]
    TypeAndClassHandler.unitTyp myTyp;
    [SerializeField]
    TypeAndClassHandler.unitClass myClass;

    TypeAndClassEventHandler typeAndClassEventHandler;
    private void Update()
    {
        CheckHealthPoints();
    }

    private void Start()
    {
        InitializeBars();
        SetBaseValues();
        currentHealthPoints = maxHealthPoints;
        currentMana = startMana;
        currentShield = 0;
        typeAndClassEventHandler = TypeAndClassEventHandler.instance;
        typeAndClassEventHandler.birdBuff += setBirdBuff;
        typeAndClassEventHandler.turtleBuff += setTurtleBuff;
        typeAndClassEventHandler.spickedBuff += setSpickedBuff;
        typeAndClassEventHandler.maskedBuff += setMaskedBuff;
        typeAndClassEventHandler.hardenedBuff += setHardenedBuff;
    }

    private void SetBaseValues()
    {
        attackSpeed = baseAttackSpeed;
        attackDamage = baseAttackDamage;
        moveSpeed = baseMoveSpeed;
        range = baseRange;
        maxHealthPoints = baseHealthPoints;
    }

    // get functions
    public float GetHealthPoints()
    {
        return currentHealthPoints;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public int GetRange()
    {
        return range * 2;
    }

    public string GetTeam()
    {
        return team;
    }

    public int GetMana()
    {
        return currentMana;
    }

    public int GetMaxMana()
    {
        return maxMana;
    }

    public int GetManaProAttack()
    {
        return manaProAttack;
    }

    public TypeAndClassHandler.unitTyp GetUnitTyp()
    {
        return myTyp;
    }

    public TypeAndClassHandler.unitClass GetUnitClass()
    {
        return myClass;
    }

    // set functions

    public void SetShield(int shieldAmount)
    {
        currentShield += shieldAmount;
        UpdateShieldBar();
    }

    public void TakeDamage(int damage)
    {
        if (currentShield == 0)
        {
            currentHealthPoints -= damage;
            UpdateHealthBar();
        }
        else
        {
            currentShield -= damage;
            if (currentShield < 0)
            {
                currentHealthPoints += currentShield;
                currentShield = 0;
                UpdateHealthBar();
            }
            UpdateShieldBar();
        }

    }

    public void AddMana(int mana)
    {
        currentMana += mana;
        UpdateManaBar();
    }

    public void ResetMana()
    {
        currentMana = 0;
        UpdateManaBar();
    }

    public void addHealth(float amount)
    {
        currentHealthPoints += (int)amount;
        UpdateHealthBar();
    }

    public void setDamage(int damage)
    {
        attackDamage = damage;
    }


    // health-, shield-, manabar functions 
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float scale = (float)currentHealthPoints / maxHealthPoints;
            healthBar.localScale = new Vector3(scale, 1f);
        }
        else
        {
            Debug.Log("No Healthbar Found");
        }
    }

    private void UpdateManaBar()
    {
        if (manaBar != null)
        {
            float scale = (float)currentMana / maxMana;
            manaBar.localScale = new Vector3(scale, 1f);
        }
        else
        {
            Debug.Log("No Healthbar Found");
        }
    }

    private void UpdateShieldBar()
    {
        if (shieldBar != null)
        {
            float scale = (float)currentShield / maxHealthPoints;
            if (scale > 1f)
            {
                scale = 1f;
            }
            shieldBar.localScale = new Vector3(scale, 1f);
        }
        else
        {
            Debug.Log("No Healthbar Found");
        }
    }

    private void InitializeBars()
    {
        healthBarHolder = transform.Find("HealthBar");
        manaBarHolder = transform.Find("ManaBar");
        shieldBarHolder = transform.Find("ShieldBar");
        if (healthBarHolder != null)
        {
            healthBar = healthBarHolder.Find("Bar");
            healthBar.localScale = new Vector3(1f, 1f);
        }
        else
        {
            Debug.Log("No 'HealthBarHolder' transform was foung");
        }
        if (manaBarHolder != null)
        {
            manaBar = manaBarHolder.Find("Bar");
            if (manaBar != null)
            {
                float scale = (float)startMana / maxMana;
                manaBar.localScale = new Vector3(scale, 1f);
            }
        }
        else
        {
            Debug.Log("No 'ManaBarHolder' transform was foung");
        }
        if (shieldBarHolder != null)
        {
            shieldBar = shieldBarHolder.Find("Bar");
            if (shieldBar != null)
            {
                shieldBar.localScale = new Vector3(0f, 1f);
            }
        }
        else
        {
            Debug.Log("No 'ShieldBarHolder' transform was foung");
        }
    }

    // check unit functions
    private void CheckHealthPoints()
    {
        if (currentHealthPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    // buffs
    public void setBirdBuff(object sender, TypeAndClassEventHandler.AmountOfUnits args)
    {
        if(myClass == TypeAndClassHandler.unitClass.bird)
        {
            attackSpeed -= 0.1f * args.GetAmount();
        }
    }

    public void setTurtleBuff(object sender, TypeAndClassEventHandler.AmountOfUnits args)
    {
        if(myClass == TypeAndClassHandler.unitClass.turtle)
        {
            moveSpeed += 0.2f;
            SetShield(100 * args.GetAmount());
        }
    }

    public void setMaskedBuff(object sender, TypeAndClassEventHandler.AmountOfUnits args)
    {
        if (myTyp == TypeAndClassHandler.unitTyp.masked)
        {
            range += 1 * args.GetAmount();
        }
    }

    public void setHardenedBuff(object sender, TypeAndClassEventHandler.AmountOfUnits args)
    {
        if (myTyp == TypeAndClassHandler.unitTyp.hardened)
        {
            maxHealthPoints += 100 * args.GetAmount();
            currentHealthPoints = maxHealthPoints;
            UpdateHealthBar();
            UpdateShieldBar();
        }
    }

    public void setSpickedBuff(object sender, TypeAndClassEventHandler.AmountOfUnits args)
    {
        if (myTyp == TypeAndClassHandler.unitTyp.spicked)
        {
            attackDamage += 10 * args.GetAmount();
        }
    }
}
