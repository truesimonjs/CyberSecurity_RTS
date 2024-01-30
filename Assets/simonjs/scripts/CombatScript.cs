using UnityEngine;
using UnityEngine.AI;

public class CombatScript : MonoBehaviour
{

    UnitScript target;
    private NavMeshAgent agent;
    UnitScript owner;
    private float nextFire;
    private Vector3 origin;
    private void Start()
    {
        owner = GetComponent<UnitScript>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void endCombat()
    {
        agent.ResetPath();
        target = null;

    }
    public bool HasTarget()
    {


        if (target == null && !findTarget())
        {
            return false;
        }
        else
        {
            if (Vector3.Distance(origin, target.transform.position) < owner.stats.leash)
            {
                AttackTarget(target);
                return true;
            }
            
            if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
            {
                endCombat();
                return false;

            }
            return true;     
            

            
        }
    }
    public void AttackTarget(UnitScript target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= owner.stats.range)
        {
            if (nextFire < Time.time)
            {
                agent.ResetPath();

                owner.animScript.Attack();
                target.Damage(owner.stats.damage);
                nextFire = Time.time + owner.stats.attackCD;
            }
        }
        else
        {
            agent.SetDestination(target.transform.position);
            
        }
    }
    public bool findTarget()
    {
        Collider[] unitsInRange = Physics.OverlapSphere(transform.position, owner.stats.aggro, LayerMask.GetMask("Unit"));

        for (int i = 0; i < unitsInRange.Length; i++)
        {
            UnitScript unit = unitsInRange[i].GetComponent<UnitScript>();
            if (unit.team != owner.team)
            {

                target = unit;
                origin = transform.position;
                return true;
            }
        }
        return false;
    }

}
