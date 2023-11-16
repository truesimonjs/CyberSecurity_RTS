using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State :  MonoBehaviour
{
    internal  GameObject owner;
    public int panelIndex;
    internal Transform targetT;
    internal Vector3 targetPos;
    private void Awake()
    {
        owner = transform.parent.gameObject;
        transform.parent.GetComponent<UnitScript>().PanelStates[panelIndex] = this;
    }
    public virtual void StateUpdate()
    {
        
    }
    public virtual void StateEnter(Transform target)
    {
        targetT = target;
        targetPos = targetT.position;
        

    }
    public virtual void StateExit()
    {

    }
    
 
}


