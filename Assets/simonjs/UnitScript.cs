using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    
    public State[] PanelStates = new State[15];
    public State currentState;
    public Transform followTarget;
    
    private void Start()
    {
       
    }

    private void Update()
    {
        
        currentState?.StateUpdate();
    }
    public void SetState(int index)
    {
        currentState = PanelStates[index];
        currentState.StateEnter(followTarget);
    }

    
}
