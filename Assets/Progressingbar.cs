using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progressingbar : MonoBehaviour

{
    public float FillAmount;
    public float totaltime;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Progressbar());
    }

    // Update is called once per frame
    void Update()

    {
        GetComponent<Image>().fillAmount = FillAmount;
    }

    IEnumerator Progressbar()
    {
        while(FillAmount<1)
        {
            FillAmount = Time.time / totaltime;
            yield return new WaitForSeconds(1);
        }
            
    }
}
