using UnityEngine;
public class IdleState : State
{
    public IdleState()
    {
        needsInput = false;
    }
    public override void StateExit()
    {
        base.StateExit();
        
    }
}
