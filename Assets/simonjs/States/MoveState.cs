using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    private NavMeshAgent agent;
    private bool targetIsTransform;
    Transform targetT;
    Vector3 targetPos;
    public override void StateEnter()
    {
        targetT = order.TargetT;
        targetPos = order.vectorTarget;
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
    public override void StateExit()
    {
        
        agent.ResetPath();
    }
}
