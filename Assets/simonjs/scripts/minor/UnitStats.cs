using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "unitStats", menuName = "scriptableobjects/unitStats")]
public class UnitStats : ScriptableObject
{
    public float range = 5;
    public float damage = 1;
    public float maxHp = 10;
    public float attackCD = 1;
    //attackmove behaviour
    public float aggro = 5;
    public float leash = 10;

}
