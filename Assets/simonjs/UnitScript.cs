using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{

    public State[] PanelStates = new State[15];
    public State currentState;
    public List<UnitOrder> Queue = new List<UnitOrder>();
    public Transform followTarget;
    public GameObject selectDisplay;
    private LineRenderer orderLine;
    //temp var for debug
    public int listcount;
    private void Start()
    {
        orderLine = GetComponentInChildren<LineRenderer>();
        currentState = PanelStates[1]; //1 is idlestate
    }

    private void Update()
    {
        
        currentState?.StateUpdate();
        listcount = Queue.Count;
        ReloadMarkers();
    }
   
    public void AddState(UnitOrder order,bool replaceCurrent = true)
    {

       if (replaceCurrent)
        {
            Queue.Clear();
            Queue.Add(order);
            NextState();
        } else 
        { 
            Queue.Add(order);
        }
       
    }
   
    public void NextState()
    {
        currentState.StateExit();
        
        if (Queue.Count>0)
        {
            UnitOrder order = Queue[0];
            currentState = order.GetState(PanelStates);
            //currentState = PanelStates[Queue[0].index];


            Queue.RemoveAt(0);
            currentState.StateEnter(order);
            
        }
        else
        {
            currentState = PanelStates[1];
            currentState.StateEnter();
        }
       
        
    }
    private void OnMouseDown()
    {
        UnitPanel.instance.selectUnit(this);

    }
    private void ReloadMarkers()
    {
        orderLine.positionCount = Queue.Count+2;
        orderLine.SetPosition(0, this.transform.position);
        orderLine.SetPosition(1, currentState.targetPos+ Vector3.up * transform.position.y);
        
        for (int i = 2; i <Queue.Count+2; i++)
        {
            orderLine.SetPosition(i, Queue[i-2].vectorTarget+Vector3.up*transform.position.y);
        }
    }
}

