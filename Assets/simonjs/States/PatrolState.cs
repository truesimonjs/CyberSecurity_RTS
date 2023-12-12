using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private NavMeshAgent agent;
   

    public override void StateEnter()
    {
        
        if (owner.Queue.Count == 0)
        {
            owner.AddState(new UnitOrder(panelIndex, transform.position),false);
            Debug.Log("t");
        }
        agent = owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPos);
        Debug.Log(owner.GetComponent<NavMeshAgent>());



    }
    public override void StateUpdate()
    {
        base.StateUpdate();
       if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            owner.AddState(new UnitOrder(panelIndex, targetPos),false);
            owner.NextState();

        }
        

    }
    public override void StateExit()
    {

        agent.ResetPath();
    }
}
