using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "team", menuName = "scriptableobjects/Teams")]
public class TeamManager : ScriptableObject
{
    public string teamName;
    public bool isPlayer = false;
    public Color teamColor = Color.white;

}
