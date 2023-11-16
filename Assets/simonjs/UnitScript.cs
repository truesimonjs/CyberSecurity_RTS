using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    
    [HideInInspector]public State[] PanelStates = new State[15];
    public State currentState;
    public Transform followTarget;
    
    private void Start()
    {
        currentState = new MoveState();
        currentState.StateEnter(this.gameObject, followTarget);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }
    public void SetState(int index)
    {

    }

    private void OnValidate()
    {
        if (PanelStates.Length!=15)
        {
            System.Array.Resize(ref PanelStates, 15);
            Debug.LogWarning("lad vær med at rod med array størrelsen!!!");
        }
    }
}
