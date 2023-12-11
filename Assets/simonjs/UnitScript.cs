using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{

    public State[] PanelStates = new State[15];
    public State currentState;
    public List<UnitOrder> Queue = new List<UnitOrder>();
    public Transform followTarget;


    private void Start()
    {
        currentState = PanelStates[1]; //1 is idlestate
    }

    private void Update()
    {

        currentState?.StateUpdate();
    }
   
    public void SetState(UnitOrder order,bool replaceCurrent = true)
    {
        if (replaceCurrent)
        {
            currentState.StateExit();
            currentState = PanelStates[order.index];
            currentState.StateEnter(order);
        }
        else
        {
            Queue.Add(order);
        }
    }
    public void NextState()
    {
        if (Queue.Count > 0)
        {
            SetState(Queue[0]);
            Queue.RemoveAt(0);
        }
    }
    private void OnMouseDown()
    {
        UnitPanel.instance.selectUnit(this);

    }
}

