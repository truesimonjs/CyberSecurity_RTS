using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    
    public bool isPlayer = false;
    public Color teamColor = Color.white;
    public int ressource = 100;
    public Ressources storage;
    private void Update()
    {
       
    }
    public bool CanAfford(Ressources cost)
    {
        return cost.value1 <= storage.value1 && cost.value2 <= storage.value2;
    }
    public void ModifyRessources(Ressources cost)
    {
        storage.value1 += cost.value1;
        storage.value2 += cost.value2;
    }

}
[System.Serializable]
public struct Ressources
{
    public int value1;
    public int value2;
    public Ressources(int value1 = 0, int value2 = 0)
    {
        this.value1 = value1;
        this.value2 = value2;
    }
    public static Ressources operator -(Ressources a) => new Ressources(-a.value1, -a.value2);
    
  
}
