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

    private void Update()
    {
        CheckHealthPoints();
    }

    private void Start()
    {
        InitializeBars();
        currentHealthPoints = maxHealthPoints;
        currentMana = startMana;
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

    public void TakeDamage(int damage)
    {
        currentHealthPoints -= damage;
        UpdateHealthBar();
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

    private void InitializeBars()
    {
        healthBarHolder = transform.Find("HealthBar");
        manaBarHolder = transform.Find("ManaBar");
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
    }
}
