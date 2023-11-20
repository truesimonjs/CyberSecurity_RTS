using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    private NavMeshAgent agent;
    private bool targetIsTransform;
    public override void StateEnter()
    {
        
        targetIsTransform = targetT != null;
        agent = owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPos);
      
        
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (targetIsTransform)
        {
            
            agent.SetDestination(targetT.position);
        }
       

        
    }
}
