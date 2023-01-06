using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mutant", menuName = "Mutant/Create New Mutant")]
public class Mutant : ScriptableObject
{
    public GameObject mutantPrefab;
    public int healthPoints;
    public float attackSpeed;
    public float movespeed;
    public int attackDamage;
    public int range;
    public string team;

    public GameObject InstantiateMutant()
    {
        GameObject unit = Instantiate(this.mutantPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        unit.GetComponent<UnitStats>().SetBaseValues(this.attackSpeed, this.attackDamage, this.movespeed, this.range, this.healthPoints, this.team);
        return unit;
    }

    public void UploadDataIntoMutant(GameObject prefab, float attackSpeed, int attackDamage, float moveSpeed, int range, int healthPoints, string team)
    {
        this.mutantPrefab = prefab;
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
        this.movespeed = moveSpeed;
        this.range = range;
        this.healthPoints = healthPoints;
        this.team = team;
    }
}
