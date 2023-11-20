using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public UnitScript selected;
    int SelectedState = -1;
    public static UnitPanel instance;
    private void Awake()
    {
        instance = this;
    }
    public void PressButton(int id)
    {
        SelectedState = id;
        StartCoroutine(CheckClick());


    }

    public void selectUnit(UnitScript unit)
    {
        if (SelectedState == -1)
        {
            selected = unit;
        }
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
                    selected.SetState(SelectedState, hit.collider.gameObject.transform);
                }
                else
                {
                    selected.SetState(SelectedState,hit.point);
                }
                SelectedState = -1;
                
            }
            yield return null;
        }
        
    }
}
