using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoarderControl : MonoBehaviour
{
    public float panSpeed;
    public float screenHeight;
    public float screenWidth;

    private Vector3 forward;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

    }

    // Update is called once per frame
    void Update()
    {
        //Forward
        if(Input.mousePosition.y > screenHeight) { Debug.Log("Front"); transform.position += Vector3.forward * Time.deltaTime; }

        //Back
        if (Input.mousePosition.y < 0) { Debug.Log("Back"); transform.position += Vector3.back * Time.deltaTime; }

        //Right
        if (Input.mousePosition.x > screenWidth) { Debug.Log("Right"); transform.position += Vector3.right * Time.deltaTime; }

        //Left
        if(Input.mousePosition.x < 0) { Debug.Log("Left"); transform.position += Vector3.left * Time.deltaTime; }
    }
}
