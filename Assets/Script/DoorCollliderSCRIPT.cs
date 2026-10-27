using UnityEngine;

public class DoorCollliderScript : MonoBehaviour
{


    public DoorInteractionScript Parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Parent == null)
            Debug.LogError("Hey this guy needs a parent");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Debug.Log(other.name);
            Parent.PlayerHere();
           // other.GetComponentInParent<PlayerController>().SetTextMethod("Press E to open or close  doors");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(collision.gameObject.tag);
    }

    private void OnCollisionExit(Collision collision)
    {
       // Debug.Log(collision.gameObject.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            Parent.PlayerLeft();
            //other.GetComponentInParent<PlayerController>().SetTextMethod("");

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
