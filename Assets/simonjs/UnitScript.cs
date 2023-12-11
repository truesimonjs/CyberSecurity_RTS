using UnityEngine;

public class UnitScript : MonoBehaviour
{

    public State[] PanelStates = new State[15];
    public State currentState;
    public Transform followTarget;


    private void Start()
    {
        currentState = PanelStates[1]; //1 is idlestate
    }

    private void Update()
    {

        currentState?.StateUpdate();
    }
   
    public void SetState(int index, UnitOrder order)
    {
        currentState.StateExit();
        currentState = PanelStates[index];
        currentState.StateEnter(order);
    }
    public void SetState(int index)
    {
        currentState.StateExit();
        currentState = PanelStates[index];
        currentState.StateEnter();
    } 
    private void OnMouseDown()
    {
        UnitPanel.instance.selectUnit(this);

    }
}

