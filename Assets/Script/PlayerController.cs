using System.Collections;
using System.Collections.Generic;
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
    public float PlayerHeight;
    public float CrouchingPlayerHeight;

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
    Rigidbody PlayerRigidbody;

    enum MovementPosition { Walking,Running,Crouching}
      MovementPosition MovementState;

    private void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        PlayerRigidbody.freezeRotation = true;
        Crouch.Enable();
        Run.Enable();
        MovementState = MovementPosition.Walking;
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


    private void StaminaManagement()
    {
        if(MovementState==MovementPosition.Running)
        {
            
            if(PlayerRigidbody.linearVelocity.magnitude>1)
            {
                Debug.Log(PlayerRigidbody.linearVelocity.magnitude);
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
            }
        }

        //Crouching
        if (MovementState == MovementPosition.Crouching)
        {
            if (Crouch.WasReleasedThisFrame())
            {
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
                PlayerRigidbody.AddForce(MoveDirection.normalized * WalkSpeed * Time.deltaTime, ForceMode.Force);

                break;
            case MovementPosition.Running:
                if (Stamina > 0)
                {
                    PlayerRigidbody.AddForce(MoveDirection.normalized * RunSpeed * Time.deltaTime, ForceMode.Force);
                    
                   // Stamina=Stamina-StaminaCost;
                }
                else
                {
                    PlayerRigidbody.AddForce(MoveDirection.normalized * WalkSpeed * Time.deltaTime, ForceMode.Force);

                }
               // Debug.Log("Runnning");
                break;
            case MovementPosition.Crouching:
                PlayerRigidbody.AddForce(MoveDirection.normalized * CrouchSpeed * Time.deltaTime, ForceMode.Force);

                //Debug.Log("Crouching");
                break;
        }
         }
   
}
