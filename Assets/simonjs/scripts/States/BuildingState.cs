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
        building = myOrder.GetComponent<Building>();
        building.Begin();
        myOrder.GetComponent<UnitScript>().team = owner.team;
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
            if (owner.team.CanAfford(building.cost))
            {
                owner.team.useRessource(building.cost);
                building.finishBuild();

            } else
            {
                Destroy(myOrder);
            }
            myOrder = null;
            owner.NextState();
        }
    }
}
