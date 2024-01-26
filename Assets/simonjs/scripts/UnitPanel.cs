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
    public List<UnitScript>[] SavedGroups = new List<UnitScript>[9];
    public List<UnitScript> debug;
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        buttons = GameObject.FindObjectsOfType<UnitButton>(true);
        towerDraw = TowerDraw.instance; //could just use instance instead of variable but i used towerdraw a lot already so i don't want to change it all right now. maybe later
    }
    private void Update()
    {
        debug = SavedGroups[1];
       if (Input.GetMouseButtonDown(1))
        {
            SelectedState=  -1;
        }
        for (int i = 0; i < SavedGroups.Length; i++)
        {
           if(SjsUtillity.GetNumDown(i))
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    
                    SavedGroups[i] = new List<UnitScript>(SelectedList);
                    continue;
                }
                if (Input.GetKey(KeyCode.LeftShift))
                {
                   
                    //SavedGroups[i].AddRange(SelectedList);
                    SjsUtillity.addRange<UnitScript>(ref SavedGroups[i], SelectedList);
                    
                    
                    continue;
                }

                if (SavedGroups[i]!=null && SavedGroups[i].Count>0)
                {

                SelectedState = -1;
                selectUnit(SavedGroups[i], false);
                }
                

            }
        }
    }
    public void PressButton(int id)
    {
        if(selected== null) 
        {
            return; 
        }
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

            AddState(SelectedList, new UnitOrder(id), !Input.GetButton("shift"));
            SelectedState = Input.GetButton("shift") ? SelectedState : -1;

        }



    }


    public void selectUnit(UnitScript unit, bool saveOld = false)
    {
        if (SelectedState == -1 && unit.team.isPlayer)
        {
            if (!Input.GetButton("shift") && !saveOld)
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
    public void selectUnit(List<UnitScript> units, bool saveOld=false)
    {
        if (!saveOld)
        {
        MassSelect(false);
        }
           
        foreach (UnitScript unit in units.ToArray())
        {
            selectUnit(unit, true);
        }
    }
    public void MassSelect(bool active)
    {
        foreach (UnitScript unit in SelectedList)
        {
            unit.GotSelected(active);
        }
        if (!active) { SelectedList.Clear(); }
    }
    private void AddState(List<UnitScript> units, UnitOrder order, bool replaceCurrent = true)
    {

        order.SetState(selected);

        if (selected.PanelStates[order.index].GetData().isBuilder)
        {
            order.vectorTarget = towerDraw.phantomTower.transform.position;
            if (!TowerDraw.instance.isBuildable)
            {
                return;
            }
            int LeastOrder = 0;

            for (int i = 1; i < units.Count; i++)
            {
                if (units[i].isIdle())
                {
                    LeastOrder = i;
                    break;
                }
                if (units[i].Queue.Count < units[LeastOrder].Queue.Count)
                {
                    LeastOrder = i;
                }
            }
                units[LeastOrder].AddState(order, replaceCurrent);
                return;
            
            

        }
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
                    AddState(SelectedList, new UnitOrder(SelectedState, hit.collider.gameObject.transform), !Input.GetButton("shift"));
                }
                else
                {
                    AddState(SelectedList, new UnitOrder(SelectedState, hit.point), !Input.GetButton("shift"));
                }
                SelectedState = Input.GetButton("shift") ? SelectedState : -1;



            }
            yield return null;
        }
        towerDraw.EndPlacement();

    }
    


}
