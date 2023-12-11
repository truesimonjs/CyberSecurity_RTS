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
    //temp var for debug
    public int listcount;
    private void Start()
    {
        currentState = PanelStates[1]; //1 is idlestate
    }

    private void Update()
    {
        
        currentState?.StateUpdate();
        listcount = Queue.Count;
    }
   
    public void SetState(UnitOrder order,bool replaceCurrent = true)
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
        if (Queue.Count > 0)
        {
            currentState = PanelStates[Queue[0].index];
            currentState.StateEnter(Queue[0]);
            Queue.RemoveAt(0);
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
}

