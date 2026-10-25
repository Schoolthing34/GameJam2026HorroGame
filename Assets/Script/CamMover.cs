using UnityEngine;

public class CamMover : MonoBehaviour
{

    public GameObject HERE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = HERE.transform.position;
        this.transform.rotation = HERE.transform.rotation;
    }
}
