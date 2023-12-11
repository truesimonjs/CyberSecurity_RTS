using System.Collections;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public UnitScript selected;
    int SelectedState = -1;
    public static UnitPanel instance;
    public GameObject[] ButtonObjects;
    private void Awake()
    {
        instance = this;

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
            selected.SetState(id);
        }
       


    }

    public void selectUnit(UnitScript unit)
    {
        if (SelectedState == -1)
        {
            selected = unit;
            for (int i = 0; i < ButtonObjects.Length; i++)
            {
                ButtonObjects[i].SetActive(selected.PanelStates[i] != null);
            }
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
                    selected.SetState(SelectedState, new UnitOrder(hit.collider.gameObject.transform));
                }
                else
                {
                    selected.SetState(SelectedState, new UnitOrder(hit.point));
                }
                SelectedState = -1;

            }
            yield return null;
        }

    }
}
