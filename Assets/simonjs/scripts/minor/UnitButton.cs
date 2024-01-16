using TMPro;
using UnityEngine;
public class UnitButton : MonoBehaviour
{
    public int index;
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>(true);
    }
    public void buttonUpdate(StateData data)
    {
        text.text = data.iconText;

    }
    public void buttonClick()
    {
        UnitPanel.instance.PressButton(index);
    }
}
