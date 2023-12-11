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
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (owner.Queue.Count > 0)
        {
            owner.NextState();
        }
    }
}
