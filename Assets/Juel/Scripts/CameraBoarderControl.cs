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
        if (Input.mousePosition.y > screenHeight) { transform.position += Vector3.forward * Time.deltaTime * panSpeed; }

        //Back
        if (Input.mousePosition.y < 0) { transform.position += Vector3.back * Time.deltaTime * panSpeed; }

        //Right
        if (Input.mousePosition.x > screenWidth) { transform.position += Vector3.right * Time.deltaTime * panSpeed; }

        //Left
        if (Input.mousePosition.x < 0) { transform.position += Vector3.left * Time.deltaTime * panSpeed; }
    }
}
