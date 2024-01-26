using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMakerState : State
{
    public GameObject prefab;
    private static StateData data = new("U");
    public Ressources cost;
    public float buildTime;
    public bool isWorking = false;
    public float nextFinish = -1;
    public override void StateEnter()
    {
        Debug.Log("test");
        if (owner.team.CanAfford(cost))
        {
            nextFinish = Time.time + buildTime;
            isWorking = true;
            owner.team.ModifyRessources(-cost);
        }
        else
        {
            owner.NextState();
        }
        
            
           
    }

    public override StateData GetData()
    {
        return data;
    }
    public override void StateExit()
    {
        if (isWorking)
        {

        isWorking = false;
        }
        base.StateExit();
    }

    public override void StateUpdate()
    {
        if (isWorking&&nextFinish<Time.time)
        {
            isWorking = false;
            UnitScript unit = Instantiate(prefab,transform.position,Quaternion.identity).GetComponent<UnitScript>();
            unit.team = owner.team;
            owner.NextState();
            unit.AddState(new UnitOrder(typeof(MoveState),this.targetPos));
        }
    }
}
