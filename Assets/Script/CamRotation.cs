using UnityEngine;

public class CamRotation : MonoBehaviour
{
    public float XSens;
    public float YSens;
     float yRotation;
     float xRotation;
    public GameObject Orientation;
    private bool CamOn=true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void TurnCamOffOrOn()
    {
        if(CamOn)
        {
            CamOn = false;
        }
        else
        {
            CamOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * XSens;
        float MouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * YSens;

        yRotation += MouseX;
        xRotation -= MouseY;
       // Debug.Log(MouseX + " and  " + MouseY);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        // transform.Rotate(new Vector3(-mouse * 0.3f, 0, 0));
        this.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
