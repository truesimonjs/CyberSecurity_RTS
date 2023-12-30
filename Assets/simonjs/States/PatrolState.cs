using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private NavMeshAgent agent;
    private Vector3 originPos;
    private bool wasFirst; //bool that determines wether or not this state was the first and thus wether to add originpos as a patrol state
    public override void StateEnter()
    {
        wasFirst = owner.Queue.Count == 0;
        if (wasFirst)
        {
            
            originPos = transform.position;
            
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
            if (wasFirst)
            {
                owner.AddState(new UnitOrder(panelIndex, originPos), false); 
            }
            owner.AddState(new UnitOrder(panelIndex, targetPos),false);  //adds itself to the back of the queue
            owner.NextState();

        }
        

    }
    public override void StateExit()
    {
        
        agent.ResetPath();
    }
}
