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
    TypeAndClassHandler.unitTyp myTyp;
    TypeAndClassHandler.unitClass myClass;
}
