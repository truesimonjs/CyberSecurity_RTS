using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    private NavMeshAgent agent;
    private bool targetIsTransform;
    
    public override void StateEnter()
    {
        Debug.Log("stateenter");
        targetIsTransform = targetT != null;
        agent = owner.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPos);
        Debug.Log(owner.GetComponent<NavMeshAgent>());
        


    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (targetIsTransform)
        {
            
            agent.SetDestination(targetT.position);
        } else if (!agent.pathPending&&agent.remainingDistance<agent.stoppingDistance)
        {
            owner.NextState();
        }
        Debug.Log(agent.pathPending && agent.remainingDistance < agent.stoppingDistance);
      
    }
    public override void StateExit()
    {
        
        agent.ResetPath();
    }
}
