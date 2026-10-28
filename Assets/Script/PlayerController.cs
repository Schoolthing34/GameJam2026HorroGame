using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{



    [Header("Movement")]
  
    public float WalkSpeed;
    [Header("Running Statistics")]
    public float RunSpeed;
    public float Stamina;
    //cost and gain should be immagined as per second so every second how muych do we gain and lose
    public float StaminaCost;
    public float StaminaGain;
    public float MaxStamina;

    [Header("Crouching Statistics")]
    public float CrouchSpeed;
   
    public float CrouchingPlayerHeight;
    public float PlayerHeight;
    [Header("Other Walking Staistics")]
    public float MaxSpeed;
    public float Drag;

 
    public Transform orientation;
    float HorizontalInput;
    float VerticalInput;
 
    Vector3 MoveDirection;

    [Header("PlayerInputs")]
    public InputAction Crouch;
    public InputAction Run;
    public InputAction OpenDoor;
    Rigidbody PlayerRigidbody;

    [Header("UI")]
    public TextMeshProUGUI PlayerbottomScreenText;


    public bool PlayerhaSeenaDoor = false;

    enum MovementPosition { Walking,Running,Crouching}
      MovementPosition MovementState;


    [Header("Slope")]
    public float maxSlopeAngle;
    private RaycastHit SlopeHit;
    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerRigidbody.freezeRotation = true;
        Crouch.Enable();
        Run.Enable();
        MovementState = MovementPosition.Walking;
        PlayerHeight = this.transform.localScale.y;
        OpenDoor.Enable();
    }
    private void Update()
    {
        PlayerInput();
        //This needs to be called in an update in order to work on fram inputs
        StaminaManagement();
        PlayerRigidbody.linearDamping = Drag;
      // Vector3 Veloctity = new Vector3(PlayerRigidbody.angularVelocity.x,0f,PlayerRigidbody.angularVelocity.z);
      //  Debug.Log(Veloctity.magnitude);
    }
    [ContextMenu("SetTextMethod")]
    public void SetTextMethod(string Input)
    {
        PlayerbottomScreenText.text = Input;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Doors"))
        {
            if (PlayerhaSeenaDoor == false)
            {
                PlayerhaSeenaDoor = true;
                SetTextMethod("Press E to Open and close doors");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Doors"))
        {
            if (PlayerhaSeenaDoor == false)
            {
                PlayerhaSeenaDoor = true;
                SetTextMethod("Press E to Open and close doors");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Doors"))
        {
            if (PlayerhaSeenaDoor)
            {

                SetTextMethod("");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Doors"))
        {
            if (PlayerhaSeenaDoor)
            {
               
                SetTextMethod("");
            }
        }
    }



    private void StaminaManagement()
    {
        if(MovementState==MovementPosition.Running)
        {
            
            if(PlayerRigidbody.linearVelocity.magnitude>1)
            {
                //Debug.Log(PlayerRigidbody.linearVelocity.magnitude);
                //Stamina Reduction
                float cost = StaminaCost * Time.deltaTime;
                //Debug.Log("cost= "+cost+" time is "+Time.deltaTime);
                Stamina -= cost;
                if (Stamina < 0)
                {
                    Stamina = 0;
                }
            }
           

        }
        else
        {
            float gain = StaminaGain * Time.deltaTime;

            Stamina += gain;

            if(Stamina>MaxStamina)
            {
                Stamina = MaxStamina;
            }
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
        //need to check if running walking or crouching

        //WHAT HAPPENS IF A PLAYER just desiced to run while crouching need tkaddress this as will have problems leaving this
        //running

        

        if (MovementState == MovementPosition.Walking)
        {
            if (Run.WasPerformedThisDynamicUpdate())
            {
                MovementState = MovementPosition.Running;
            }
            if (Crouch.WasPerformedThisDynamicUpdate())
            {
                MovementState = MovementPosition.Crouching;
                PlayerCrouchTransform(true);
            }
        }

        //Crouching
        if (MovementState == MovementPosition.Crouching)
        {
            if (Crouch.WasReleasedThisFrame())
            {
                PlayerCrouchTransform(false);
                MovementState = MovementPosition.Walking;
            }
        }
        if (MovementState == MovementPosition.Running)
        {
            if (Run.WasReleasedThisFrame())
            {
                MovementState = MovementPosition.Walking;
            }
        }

        //Opening door/ interaction with world stuff
        if (OpenDoor.WasPerformedThisDynamicUpdate())
        {
            Debug.Log("e PRESSED");
            //Check for a door to open
            RaycastHit ray;
            Vector3 Foward = transform.TransformDirection(this.transform.position);
            Debug.DrawRay(this.transform.position, Foward * 100f, Color.aliceBlue, 1000);
            if (Physics.Raycast(this.transform.position, Foward, out ray, 10))
            {
                Debug.Log(ray.collider.name);
               // Debug.DrawRay(this.transform.position, Foward*100f,Color.aliceBlue,1000);
                if (ray.collider.CompareTag("Doors"))
                {
                    Debug.Log(ray.collider.name);
                }
            }
        }
    }
    private void PlayerCrouchTransform(bool Crouch)
    {
        if (Crouch)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, CrouchingPlayerHeight, this.transform.localScale.z);
        }
        else
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, PlayerHeight, this.transform.localScale.z);
        }
    }
    private void MovePlayer()
    {

       
        //get movement direction using the orientation and the input
        MoveDirection=orientation.forward*VerticalInput+orientation.right*HorizontalInput;
        // if()

        //Crouch.AddBinding("c");

        switch (MovementState)
        {
            case MovementPosition.Walking:
                //Debug.Log("Walking");
                if (OnSlope())
                {
                    PlayerRigidbody.AddForce(GetSlopeMoveDirection() * WalkSpeed * Time.deltaTime, ForceMode.Force);
                }
                else
                {
                    PlayerRigidbody.AddForce(MoveDirection.normalized * WalkSpeed * Time.deltaTime, ForceMode.Force);
                }
                break;
            case MovementPosition.Running:
                if (Stamina > 0)
                {
                    if (OnSlope())
                    {
                        PlayerRigidbody.AddForce(GetSlopeMoveDirection() * RunSpeed * Time.deltaTime, ForceMode.Force);
                }
                    else
                    {
                        PlayerRigidbody.AddForce(MoveDirection.normalized * RunSpeed * Time.deltaTime, ForceMode.Force);
                    }
                    
                   // Stamina=Stamina-StaminaCost;
                }
                else
                {
                    if (OnSlope())
                    {
                        PlayerRigidbody.AddForce(GetSlopeMoveDirection() * WalkSpeed * Time.deltaTime, ForceMode.Force);
}
                    else
                    {
                        PlayerRigidbody.AddForce(MoveDirection.normalized * WalkSpeed * Time.deltaTime, ForceMode.Force);
                    }
                  
                }
               // Debug.Log("Runnning");
                break;
            case MovementPosition.Crouching:
                if (OnSlope())
                {
                    PlayerRigidbody.AddForce(GetSlopeMoveDirection() * CrouchSpeed * Time.deltaTime, ForceMode.Force);
}
                else
                {
                    PlayerRigidbody.AddForce(MoveDirection.normalized * CrouchSpeed * Time.deltaTime, ForceMode.Force);
                }
               
                //Debug.Log("Crouching");
                break;
        }
         }
   

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position,Vector3.down,out SlopeHit,PlayerHeight*0.5f+1f))
        {
            float angle = Vector3.Angle(Vector3.up, SlopeHit.normal);
            //Debug.Log(angle);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }


    private  Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(MoveDirection, SlopeHit.normal).normalized;
    }
}
