using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    
    public bool isPlayer = false;
    public Color teamColor = Color.white;
    public int ressource = 100;
    public ressources storage;
    public bool CanAfford(ressources cost)
    {
        return cost.value1 <= storage.value1 && cost.value2 <= storage.value2;
    }
    public void useRessource(ressources cost)
    {
        storage.value1 -= cost.value1;
        storage.value2 -= cost.value2;
    }

}
[System.Serializable]
public struct ressources
{
    public int value1;
    public int value2;
    public ressources(int value1 = 0, int value2 = 0)
    {
        this.value1 = value1;
        this.value2 = value2;
    }
 
  
}
