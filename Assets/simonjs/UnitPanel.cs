using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            selected.AddState(new UnitOrder(id),!Input.GetButton("shift"));

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

            selected?.selectDisplay.SetActive(false);
            selected = unit;
            selected.selectDisplay.SetActive(true);
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
    public IEnumerator CheckClick()
    {
        while (SelectedState != -1)
        {
            if (Input.GetMouseButtonDown(0))
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, Mathf.Infinity);
                if (LayerMask.NameToLayer("Unit") == hit.collider.gameObject.layer)
                {
                    Debug.Log("target was unit");
                    selected.AddState(new UnitOrder(SelectedState,hit.collider.gameObject.transform), !Input.GetButton("queue"));
                }
                else
                {
                    selected.AddState(new UnitOrder(SelectedState,hit.point), !Input.GetButton("queue"));   
                }
                SelectedState = Input.GetButton("shift")? SelectedState:-1;

            }
            yield return null;
        }

    }
}
