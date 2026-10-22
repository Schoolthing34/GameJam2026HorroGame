using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{


    public  enum ButtonType{Start,SettingButton,ExitButton};
    public ButtonType Type = ButtonType.Start;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void pressButton()
    {
        if(Type == ButtonType.Start)
        {
            SceneManager.LoadScene("HorrorLevel");
        }
        if (Type == ButtonType.SettingButton)
        {
           Debug.Log("Dave Mathews");
        }
        if (Type == ButtonType.ExitButton)
        {
            Debug.Log("Dave Mathews2");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
