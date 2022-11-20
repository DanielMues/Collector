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

    public void Update()
    {
        CheckHealthPoints();
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
}
