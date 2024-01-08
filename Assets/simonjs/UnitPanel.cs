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
    private void Awake()
    {

        instance = this;
        buttons = GameObject.FindObjectsOfType<UnitButton>(true);

    }
    public void PressButton(int id)
    {
        SelectedState = id;
        
        if (selected.PanelStates[id].needsInput)
        {
            StartCoroutine(CheckClick());
        }
        else
        {

            AddState(SelectedList,new UnitOrder(id),!Input.GetButton("shift"));

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
            unit.selectDisplay.SetActive(true);
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
            unit.selectDisplay.SetActive(active);
        }
        if (!active) { SelectedList.Clear(); }
    }
    private void AddState(List<UnitScript> units,UnitOrder order, bool replaceCurrent = true)
    {
        foreach (UnitScript unit in units)
        {
            unit.AddState(order, replaceCurrent);
        }
    }
    public IEnumerator CheckClick()
    {
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
            }
            yield return null;
        }

    }
}
