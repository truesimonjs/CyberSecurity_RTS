using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour, IDamageable
{
    public float Hp;
    [HideInInspector]public State[] PanelStates = new State[15];
    private State currentState;
    public List<UnitOrder> Queue = new List<UnitOrder>();
    //references
    public GameObject selectDisplay;
    private LineRenderer orderLine;
    [HideInInspector] public CombatScript combatscript;
    public UnitAnim animScript;
    //
    public UnitStats stats;
    private bool isSelected;
    //public Team team;
    public TeamManager team;
  
    private void Awake()
    {
        Hp = stats.maxHp;
    }
    private void Start()
    {
        orderLine = GetComponentInChildren<LineRenderer>();
        combatscript = GetComponent<CombatScript>();
        currentState = PanelStates[1]; //1 is idlestate
        
        

    }

    private void Update()
    {

        currentState?.StateUpdate();
        
        if (isSelected)
        {
            ReloadMarkers();
        }
    }

    public void AddState(UnitOrder order, bool replaceCurrent = true)
    {

        if (replaceCurrent)
        {
            Queue.Clear();
            Queue.Add(order);
            NextState();
        }
        else
        {
            Queue.Add(order);
        }

    }

    public void NextState()
    {
        currentState?.StateExit();

        if (Queue.Count > 0)
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
    public void GotSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        selectDisplay.SetActive(isSelected);
        orderLine.enabled = isSelected;

    }
    private void OnMouseDown()
    {
        UnitPanel.instance.selectUnit(this);

    }
    private void ReloadMarkers()
    {
        if (!isIdle())
        {


            orderLine.positionCount = Queue.Count + 2;
            orderLine.SetPosition(0, this.transform.position);
            orderLine.SetPosition(1, currentState.targetPos + Vector3.up * 2);

            for (int i = 2; i < Queue.Count + 2; i++)
            {
                orderLine.SetPosition(i, Queue[i - 2].vectorTarget + Vector3.up * 2);
            }
        }
        else
        {
            orderLine.positionCount = 0;
        }
    }
    public bool isIdle()
    {
        //Debug.Log(currentState == PanelStates[1] && Queue.Count < 1);
        return (currentState == PanelStates[1] && Queue.Count == 0);
    }
    public void Damage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
public enum Team
{
    Friendly, Hostile

}
