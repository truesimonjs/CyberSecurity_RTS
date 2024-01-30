using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Progressingbar : MonoBehaviour
{
    //Visual bar
    public float FillAmount;
    public float totaltime;

    //text
    public TextMeshProUGUI tid;
    private string formatter;
    float min = 00, sec = 00;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Progressbar());
        //tid = transform.parent.GetComponentInChildren<Text>();
        formatter = ": " + min +" : " + sec;
        

        Debug.Log(formatter);
    }

    // Update is called once per frame
    void Update()
    {
        if (totaltime - Time.time<=0)
        {
            Debug.Log("NextScene");
            SceneManager.LoadScene(1);
        }
        GetComponent<Image>().fillAmount = FillAmount;
        sec = totaltime-Time.time;
        min = (int)(sec / 60);
        sec = (int)(sec - min * 60);
        formatter = min + " : " + sec;
        tid.text = formatter;
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
