using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class DoorInteractionScript : MonoBehaviour
{

    public bool DoorOpen;

    public Vector3 OpenAngle;
    public Vector3 CloseAngle;
    public Vector3 targetAngle;
    public GameObject DoorHinge;
    public GameObject KeyNeeded;
    public bool DoorLocked;
    public bool PlayerCanNowOpenMe = false;
    public InputAction DoorInteractionButton;
    bool PlayerHasPressedButton = false;
    public float DoorMovementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DoorInteractionButton.Enable();
        //Basic garnetee doors in state we want
        if (DoorOpen)
        {
            OpenDoor();
            
        }
        else
        {
            CloseDoor();
        }
    }

    [ContextMenu("TestCloseDoor")]
    private void TestCloseDoor()
    {
        //  DoorHinge.transform.rotation = Quaternion.Euler(DoorHinge.transform.rotation.x, OpenAngle.rotation.y, DoorHinge.transform.rotation.z);
        DoorOpen = false;
        PlayerHasPressedButton = true;

        targetAngle = CloseAngle;
        DoorHinge.GetComponent<Collider>().enabled = false;
        DoorHinge.transform.rotation = Quaternion.Euler(targetAngle);
    }
    [ContextMenu("CloseDoor")]
    private void CloseDoor()
    {
      //  DoorHinge.transform.rotation = Quaternion.Euler(DoorHinge.transform.rotation.x, OpenAngle.rotation.y, DoorHinge.transform.rotation.z);
        DoorOpen = false;
        PlayerHasPressedButton = true;
        
        targetAngle = CloseAngle;
        DoorHinge.GetComponent<Collider>().enabled = false;
    }


   public void  PlayerHere()
    {
        PlayerCanNowOpenMe = true;
    }

    public void PlayerLeft()
        { PlayerCanNowOpenMe = false; }
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
    [ContextMenu("TestOpenDoor")]
    private void TestOpenDoor()
    {
        PlayerHasPressedButton = true;
        DoorOpen = true;
        targetAngle = OpenAngle;
        DoorHinge.GetComponent<Collider>().enabled = false;
        DoorHinge.transform.rotation = Quaternion.Euler(targetAngle);
    }
    [ContextMenu("OpenDoor")]
    private void OpenDoor()
    {
        PlayerHasPressedButton = true;
        DoorOpen = true;
        targetAngle = OpenAngle;
        DoorHinge.GetComponent<Collider>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerCanNowOpenMe)
        {
            if (DoorInteractionButton.WasPressedThisDynamicUpdate())
            {
                Debug.Log("Hey eeee");
                InteractWithDoor();
            }
        }

        if(PlayerHasPressedButton)
        {
            if (DoorHinge.transform.rotation != Quaternion.Euler( targetAngle))
            {
               
                DoorHinge.transform.rotation = Quaternion.RotateTowards(DoorHinge.transform.rotation,Quaternion.Euler(targetAngle), DoorMovementSpeed * Time.deltaTime);
            }
            else
            {
                DoorHinge.GetComponent<Collider>().enabled = true;
                PlayerHasPressedButton = false;
            }
        }
    }
}
