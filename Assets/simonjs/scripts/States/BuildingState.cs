using UnityEngine;
using UnityEngine.AI;

public class BuildingState : State
{
    private static StateData data = new("B", true);
    public GameObject prefab;
    private GameObject myOrder;
    private Building building;
    private NavMeshAgent agent;
    // multiple buildingtypes in 1 unit isn't possible without making empty inherited scripts as order only carries the script type
    //attempting to order a building while having selected multiple isn't supported yet, all units would likely be ordered to build at the same spot. 
    public override void Awake()
    {
        base.Awake();
        agent = owner.GetComponent<NavMeshAgent>();

    }
    public override StateData GetData()
    {
        return data;
    }

    public override BuildingState getBuilder()
    {
        return this;
    }

    public override void StateEnter()
    {
        myOrder = Instantiate(prefab, targetPos, Quaternion.identity);
        myOrder.GetComponent<Building>().Begin();
        agent.SetDestination(targetPos);
    }

    public override void StateExit()
    {
        agent.ResetPath();
        if (myOrder != null)
        {
            Destroy(myOrder);
        }
    }

    public override void StateUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            myOrder.GetComponent<Building>().finishBuild();
            myOrder = null;
            owner.NextState();
        }
    }
}
