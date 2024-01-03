using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    private static StateData myData = new StateData("M");
    private NavMeshAgent agent;
    private bool targetIsTransform;
    public override StateData GetData()
    {
        return myData;
    }
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
        } else if (!agent.pathPending&&agent.remainingDistance<agent.stoppingDistance)
        {
            owner.NextState();
        }
       // Debug.Log(agent.pathPending && agent.remainingDistance < agent.stoppingDistance);
      
    }
    public override void StateExit()
    {
        
        agent.ResetPath();
    }
}
