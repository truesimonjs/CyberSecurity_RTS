using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State :  MonoBehaviour
{
    public  GameObject owner;
    public int panelIndex;
    internal Transform targetT;
    internal Vector3 targetPos;
   
    public virtual void StateUpdate()
    {
        
    }
    public virtual void StateEnter(GameObject caller,Transform target= null)
    {
        targetT = target;
        targetPos = targetT.position;
        owner = caller;

    }
    public virtual void StateExit()
    {

    }
    
 
}

public interface IState
{
    void StateUpdate();
    void StateEnter(GameObject caller, Transform target = null);

    void StateExit();
}
