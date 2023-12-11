using UnityEngine;

public class State : MonoBehaviour
{
    internal GameObject owner;
    public int panelIndex;
    //deprecating
   // internal Transform targetT;
   // internal Vector3 targetPos;
    //
    internal UnitOrder order;
    [HideInInspector]
    public bool needsInput = true;
    private void Awake()
    {
        owner = transform.parent.gameObject;
        transform.parent.GetComponent<UnitScript>().PanelStates[panelIndex] = this;
    }
    public virtual void StateUpdate()
    {

    }
   
    public void StateEnter(UnitOrder order)
    {
        this.order = order;
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
    public UnitOrder(Transform target)
    {
        TargetT = target;
        vectorTarget = target.position;
    }
    public UnitOrder(Vector3 target)
    {
        TargetT = null;
        vectorTarget = target;
    }
    public UnitOrder()
    {


    }
}
