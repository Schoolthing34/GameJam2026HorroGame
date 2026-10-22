using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera PlayerEyes;
    public PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //we want ot make a simple thing here
        InputManager();
    }

   public void PlayerMoves(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        Debug.Log(context.control);
        Debug.Log(context.action);
       // Debug.Log(context.ReadValue());
        //  Debug.Log(playerInput.DebugLogAction());
    }

    public void RandomFunction()


    {
        //  Debug.Log(playerInput.DebugLogAction());
    }
    private void InputManager()
    {
       // if(Input.)
        //{
          //  Debug.Log("Hey");
      //  }//
    }
}
