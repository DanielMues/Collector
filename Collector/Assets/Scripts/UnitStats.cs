using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int speed;
    [SerializeField]
    private int attackDamage;
    [SerializeField]
    private int range;
    [SerializeField]
    private string team;

    public int GetHealthPoints()
    {
        return healthPoints;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public int GetRange()
    {
        return range;
    }

    public string GetTeam()
    {
        return team;
    }
}
