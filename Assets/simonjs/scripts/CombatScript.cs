using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatScript : MonoBehaviour
{
    float range = 5;
    float aggro = 5;
    float leash = 10;
    UnitScript target;
    private NavMeshAgent agent;
    UnitScript owner;
    private void Start()
    {
        owner = GetComponent<UnitScript>();
        agent = GetComponent<NavMeshAgent>();
    }
    public bool CheckCombat()
    {
        
        
        if (target==null || !findTarget() )
        {
           
        }
        return false;
    }
    public void AttackTarget(UnitScript target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= range)
        {
            agent.ResetPath();

            target.Damage(1);
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
    }
    public bool findTarget()
    {
        Collider[] unitsInRange = Physics.OverlapSphere(transform.position, aggro, LayerMask.GetMask("Unit"));
      
        for (int i = 0; i < unitsInRange.Length; i++)
        {
            UnitScript unit = unitsInRange[i].GetComponent<UnitScript>();
            if (unit.faction == Faction.Hostile)
            {

                target = unit;
                return true;
            }
        }
        return false;
    }
   
}
