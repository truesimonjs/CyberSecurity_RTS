using System;
using UnityEngine;

public class State : MonoBehaviour
{
    internal UnitScript owner;
    public int panelIndex;
    //deprecating
    internal Transform targetT;
    internal Vector3 targetPos;
    //
    internal UnitOrder order;
    [HideInInspector]
    public bool needsInput = true;
    public virtual void Awake()
    {
        owner = transform.parent.gameObject.GetComponent<UnitScript>();
        owner.PanelStates[panelIndex] = this;
        //transform.parent.GetComponent<UnitScript>().PanelStates[panelIndex] = this;
    }
    public virtual StateData GetData()
    {
        return new StateData();
    }
    //getbuilder allows scripts to access variables and methods specific to building states, having to make a method everytime you need new methods/variables isn't optimal but luckily i don't need a lot
    public virtual BuildingState getBuilder()
    {
        return null;
    }
    public virtual void StateUpdate()
    {

    }

    public void StateEnter(UnitOrder order)
    {
        targetPos = order.vectorTarget;
        targetT = order.TargetT;

        StateEnter();

    }
    public virtual void StateEnter()
    {

    }

    public virtual void StateExit()
    {

    }
    static public State findState<T>(State[] states) where T : State
    {
        foreach (State state in states)
        {
            if (state.GetType() == typeof(T))
            {
                return state;
            }
        }
        return null;
    }

}
public class StateData
{
    public string iconText;
    public bool isBuilder;
    public StateData(string iconText = " ", bool isBuilder = false)
    {
        this.isBuilder = isBuilder;
        this.iconText = iconText;
    }

}
public class UnitOrder
{
    public Vector3 vectorTarget;
    public Transform TargetT;
    public int index;
    public Type stateT;

    //changed to using stateetypes instead of int index, old code remains due to scripts using it
    public UnitOrder(Type stateType, Transform target)
    {
        this.stateT = stateType;
        TargetT = target;
        vectorTarget = target.position;
        constructed();
    }
    public UnitOrder(Type stateType, Vector3 target)
    {
        this.stateT = stateType;
        TargetT = null;
        vectorTarget = target;
        constructed();
    }
    public UnitOrder(Type stateType)
    {

        this.stateT = stateType;
        constructed();

    }
    public void SetState(UnitScript unit)
    {
        stateT = unit.PanelStates[index].GetType();
    }
    public State GetState(State[] states)
    {
        if (stateT == null)
        {
            // Debug.Log("unitorder made use of index");
            return states[index];
        }
        foreach (State state in states)
        {

            if (state != null && state.GetType() == stateT)
            {

                return state;
            }

        }

        return null;
    }
    public void constructed()
    {
        if (stateT.GetType() != typeof(State))
        {

            Debug.LogError("created unityorder with type that does not inherit from State");
        }
    }
    //deprecating maybe
    public UnitOrder(int index, Transform target)
    {
        this.index = index;
        TargetT = target;
        vectorTarget = target.position;
    }
    public UnitOrder(int index, Vector3 target)
    {
        this.index = index;
        TargetT = null;
        vectorTarget = target;
    }
    public UnitOrder(int index)
    {
        this.index = index;

    }
}
