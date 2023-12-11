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
    private void Awake()
    {
        owner = transform.parent.gameObject.GetComponent<UnitScript>();
        owner.PanelStates[panelIndex] = this;
        //transform.parent.GetComponent<UnitScript>().PanelStates[panelIndex] = this;
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


}

public class UnitOrder
{
    public Vector3 vectorTarget;
    public Transform TargetT;
    public int index;
    public UnitOrder(int index,Transform target)
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
