using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : State
{
    private static StateData data = new("B",true);
    public GameObject prefab;

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
        base.StateEnter();
    }

    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }
}
