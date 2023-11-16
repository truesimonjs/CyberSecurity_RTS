using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPanel : MonoBehaviour
{
    public UnitScript selected;
    int SelectedState = -1;
   public void PressButton(int id)
    {
        selected.SetState(id);
    }
}
