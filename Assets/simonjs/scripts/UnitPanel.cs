using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPanel : MonoBehaviour
{
    public UnitScript selected;
    public List<UnitScript> SelectedList;
    int SelectedState = -1;
    public static UnitPanel instance;
    //public GameObject[] ButtonObjects;
    private UnitButton[] buttons;
    private TowerDraw towerDraw;
    private void Awake()
    {

        instance = this;
        buttons = GameObject.FindObjectsOfType<UnitButton>(true);
        towerDraw = GetComponent<TowerDraw>();

    }
    public void PressButton(int id)
    {
        SelectedState = id;
        
        if (selected.PanelStates[id].needsInput)
        {
            if (selected.PanelStates[id].GetData().isBuilder)
            {
                towerDraw.BeginPlacement(selected.PanelStates[id].getBuilder().prefab);
            }
            StartCoroutine(CheckClick());
        }
        else
        {

            AddState(SelectedList,new UnitOrder(id),!Input.GetButton("shift"));
            SelectedState = Input.GetButton("shift") ? SelectedState : -1;

        }
       


    }


    public void selectUnit(UnitScript unit)
    {
        if (SelectedState == -1)
        {
            if (!Input.GetButton("shift"))
            {
                MassSelect(false);
            }

            SelectedList.Add(unit);
            selected = SelectedList[0];
            unit.GotSelected(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                UnitButton button = buttons[i];
                
                button.gameObject.SetActive(selected.PanelStates[button.index] != null);
                if (button.gameObject.activeSelf)
                {
                    button.buttonUpdate(selected.PanelStates[button.index].GetData());
                }
            }
        }
    }
    public void MassSelect(bool active)
    {
        foreach(UnitScript unit in SelectedList)
        {
            unit.GotSelected(active);
        }
        if (!active) { SelectedList.Clear(); }
    }
    private void AddState(List<UnitScript> units,UnitOrder order, bool replaceCurrent = true)
    {
        Debug.Log(selected);
        order.SetState(selected);

        if (selected.PanelStates[order.index].GetData().isBuilder)
        {
            order.TargetT = towerDraw.phantomTower.transform;
        }
        foreach (UnitScript unit in units)
        {
            unit.AddState(order, replaceCurrent);
        }
    }
    public IEnumerator CheckClick()
    {
        bool shouldEndPlacement = true;
        while (SelectedState != -1)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())  //ispointerovergameboject returns true if you clicked on ui, i reverse it to prevent issueing commands while clicking on ui
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Mathf.Infinity);
                if (LayerMask.NameToLayer("Unit") == hit.collider.gameObject.layer)
                {
                    Debug.Log("target was unit");
                    AddState(SelectedList,new UnitOrder(SelectedState,hit.collider.gameObject.transform), !Input.GetButton("shift"));
                }
                else
                {
                    AddState(SelectedList,new UnitOrder(SelectedState,hit.point), !Input.GetButton("shift"));   
                }
                SelectedState = Input.GetButton("shift")? SelectedState:-1;

                Debug.Log(hit.point);
                shouldEndPlacement = false;
            }
            yield return null;
        }
        towerDraw.EndPlacement(shouldEndPlacement);

    }

    
}
