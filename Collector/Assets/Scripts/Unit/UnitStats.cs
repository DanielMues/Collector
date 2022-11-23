using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    // health
    [Header("Health")]
    [SerializeField]
    private int maxHealthPoints = 100;
    private Transform healthBarHolder;
    private Transform healthBar;
    private int currentHealthPoints;

    // stuff
    [Header("Stuff")]
    [SerializeField]
    private float attackSpeed = 1f;
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private int attackDamage = 1;
    [SerializeField]
    private int range = 1;
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

    private void Update()
    {
        CheckHealthPoints();
    }

    private void Start()
    {
        InitializeBars();
        currentHealthPoints = maxHealthPoints;
        currentMana = startMana;
        currentShield = 0;
    }

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
        return range*2;
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

    public void SetShield(int shieldAmount)
    {
        currentShield += shieldAmount;
        UpdateShieldBar();
    }

    public void TakeDamage(int damage)
    {
        if(currentShield == 0)
        {
            currentHealthPoints -= damage;
            UpdateHealthBar();
        }
        else {
            currentShield -= damage;
            if(currentShield < 0)
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

    private void CheckHealthPoints()
    {
        if(currentHealthPoints <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void UpdateHealthBar()
    {
        if(healthBar != null)
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
        if(manaBar != null)
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
            if(scale > 1f)
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
        if(manaBarHolder != null)
        {
            manaBar = manaBarHolder.Find("Bar");
            if(manaBar != null)
            {
                float scale = (float)startMana / maxMana ;
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

    public void addHealth(float amount)
    {
        currentHealthPoints += (int)amount;
        UpdateHealthBar();
    }
}
