using Unity.VisualScripting;
using UnityEngine;

public class AudioSphere : MonoBehaviour
{
    public GameObject itself;
    private bool MakingSounds;
    private float OriginalSize;
    public bool IsPlayerSound;
    private float timer = 1f;
    void Start()
    {
        OriginalSize = itself.transform.localScale.x;

    }
    public void SoundOn(float Size,bool playerSound=true)
    {
        IsPlayerSound = playerSound;
        itself.transform.localScale = new Vector3(Size,Size,Size);
      //  itself.transform.localScale = new Vector3(999, 999, 999);
        MakingSounds = true;



    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy")
        {
            other.GetComponent<EnemyMainMovement>().HeardSound(this.gameObject.transform.position,IsPlayerSound);
        }
    }

    private void Update()
    {
        if (MakingSounds)
        {
            timer -= Time.deltaTime;


            if(timer<=0)
            {
                timer = 1f;
                SoundOff();

            }
        }
    }

    void SoundOff()
    {
        MakingSounds = false;
        itself.transform.localScale=new Vector3(OriginalSize,OriginalSize,OriginalSize);
    }


}
