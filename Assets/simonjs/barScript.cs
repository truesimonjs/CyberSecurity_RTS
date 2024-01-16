using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barScript : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider.maxValue = 100;
        slider.minValue = 0;
        StartCoroutine(timeBar());
    }




    public IEnumerator timeBar()
    {
        while (true)
        {


            slider.value += 1;
            yield return new WaitForSeconds(1);
        }
    }
}
