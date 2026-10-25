using UnityEngine;

public class DoorInteractionScript : MonoBehaviour
{

    public bool DoorOpen;

    public float OpenAngle;
    public float CloseAngle;
    public GameObject DoorHinge;
    public GameObject KeyNeeded;
    public bool DoorLocked;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(DoorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }


    private void CloseDoor()
    {
        DoorHinge.transform.rotation = Quaternion.Euler(DoorHinge.transform.rotation.x, OpenAngle, DoorHinge.transform.rotation.z);
        DoorOpen = false;
    }
    public void InteractWithDoor()
    {
        if (DoorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }
    private void OpenDoor()
    {
        DoorHinge.transform.rotation = Quaternion.Euler(DoorHinge.transform.rotation.x, OpenAngle, DoorHinge.transform.rotation.z);
        DoorOpen = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
