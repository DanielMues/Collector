using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int range;
    [SerializeField]
    private string team;

    Transform healthBar;
    Transform bar;
    private float maxHealthPoints;

    private void Update()
    {
        CheckHealthPoints();
    }

    private void Start()
    {
        InitializeHealthBar();
        maxHealthPoints = healthPoints;
    }

    public int GetHealthPoints()
    {
        return healthPoints;
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

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        UpdateHealthBar();
    }

    private void CheckHealthPoints()
    {
        if(healthPoints <= 0)
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
        float scale = healthPoints / maxHealthPoints;
        if(bar != null)
        {
            bar.localScale = new Vector3(scale, 1f);
        }
        else
        {
            Debug.Log("No Healthbar Found");
        }
    }

    private void InitializeHealthBar()
    {
        healthBar = transform.Find("HealthBar");
        if (healthBar != null)
        {
            bar = healthBar.Find("Bar");
        }
        else
        {
            Debug.Log("No 'HealthBar' transform was foung");
        }
    }
}
