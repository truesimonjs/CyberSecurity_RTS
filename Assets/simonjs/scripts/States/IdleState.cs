using UnityEngine;
public class IdleState : State
{
    private static StateData mydata = new StateData("S");
    private CombatScript combatScript;
    public override void Awake()
    {
        base.Awake();

    }
    public IdleState()
    {
        needsInput = false;
    }
    public override void StateEnter()
    {
        base.StateEnter();

    }
    public override StateData GetData()
    {
        return mydata;
    }
    public override void StateExit()
    {
        base.StateExit();
        owner.combatscript.endCombat();

    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        targetPos = this.transform.position;
        if (owner.Queue.Count > 0)
        {
            owner.NextState();
        }
        else
        {
            owner.combatscript.HasTarget();
            Debug.Log("ran combatscript");
        }
    }
}
