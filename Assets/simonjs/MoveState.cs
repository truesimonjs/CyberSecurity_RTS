using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    private NavMeshAgent agent;
    public override void StateEnter(GameObject caller, Transform target = null)
    {
        base.StateEnter(caller, target);
        agent = owner.GetComponent<NavMeshAgent>();
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        agent.SetDestination(targetT.position);
    }
}
