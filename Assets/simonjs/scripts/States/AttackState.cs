using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private static StateData myData = new StateData("A");
    private NavMeshAgent agent;
    private bool targetIsTransform;
    private UnitScript targetUnit;
    
    private bool oldHasTarget = false;
    public override StateData GetData()
    {
        return myData;
    }

    public override void StateEnter()
    {
        targetIsTransform = targetT != null;
        agent = owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPos);
        if (targetIsTransform)
        {
            
            targetUnit = targetT.GetComponent<UnitScript>();
        }
       
    }

    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateUpdate()
    {
        if (targetIsTransform)
        {
            owner.combatscript.AttackTarget(targetUnit);
        }
        else 
        {
            bool hasTarget = owner.combatscript.HasTarget();
            if (!hasTarget&&oldHasTarget)
            {
                agent.SetDestination(targetPos);

            }
            oldHasTarget = hasTarget;
        }
    }
}
