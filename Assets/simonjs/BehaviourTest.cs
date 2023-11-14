using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTest : MonoBehaviour
{
    BaseState currentState;
    public Transform followTarget;
    private void Start()
    {
        currentState = new MoveState();
        currentState.StateEnter(this.gameObject,followTarget);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }
}
