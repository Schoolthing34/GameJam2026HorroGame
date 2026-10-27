using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RoofScript : MonoBehaviour
{

   public  GameObject[] Roofs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    [ContextMenu("RevealRoofs")]

    public void RevealRoofs()
    {
        for (int i = 0; i < Roofs.Length; i++)
        {
            Roofs[i].gameObject.SetActive(true);// = false;   
        }
    }

    [ContextMenu("RemoveRoofs")]
    public void RemoveRoofs()
    {
      
        // Dave();
        for (int i = 0; i < Roofs.Length; i++)
        {
            Roofs[i].gameObject.SetActive(false);// = false;   
        }
    }
     RoofScript() { 
       // Roofs=gameObject.GetComponent<RoofScript>().Roofs;
    }
}


