using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMainMovement : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    public Vector3 Target;
    public Transform WhereHeGoing;

    public enum MovementMode { Patrolling,JustHerdNoise,Persuing,DestroyingMusicBox,Scouting};
    public bool PlayerSound = false;
    public MovementMode EnemyMode=MovementMode.Patrolling;
    public bool DoneExploring;
    public float Timer;
    public float DescicionTimer;
    public float SoundTimer=0.5f;
    public AudioSource EnemySounds;
    public int speed;
    public Vector3 GoPosition;

    [Header("Manager")]
    public EnemyManager Manager;

    // Start is called before the first frame update
    void Start()
    {

      
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("No nav MeshAgent");
        }
        if(Target == null)
        {
            Debug.LogError("No nav NoTarget");
        }

        FindNewPosition();



    }

    [ContextMenu("FindNewPosition")]
    public void FindNewPosition()
    {
        WhereHeGoing = Manager.GetRoom();
        Target=WhereHeGoing.position;
        //Debug.Log("Oh no it aint random " + UnityEngine.Random.Range(0, PatrolPionts.Length ));
        agent.SetDestination(Target);
        // agent.CalculatePath(Target.transform.position, agent.path);


        Timer = UnityEngine.Random.Range(30,101);
        DescicionTimer = UnityEngine.Random.Range(5, 20);

    }

    public void GetNearSpot()
    {
        //this will get a near spot to the original
        Target = Target + new Vector3(UnityEngine.Random.Range(-6f, 6f), 0, UnityEngine.Random.Range(-6f, 6f));
        
        agent.SetDestination(Target);
        DescicionTimer = UnityEngine.Random.Range(5f, 20f);
    }

    private void WalkingSound()
    {
        if(agent.velocity!=Vector3.zero)
        {
            EnemySounds.Play();
        }
    }
    public void GoToPos(Vector3 playerPos)
    {
        agent.SetDestination(playerPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
       // Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Im a genius2");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Im a genius");
        }
     }

    public void HeardSound(Vector3 playerPos,bool IsPlayer)
    {
        PlayerSound = IsPlayer;
        if(IsPlayer)
        {
            //hear is get angry and look for player
            Debug.Log("Is player +anger");

            EnemyMode = MovementMode.JustHerdNoise;

        }
        else
        {
            //Hear is get angry and destroy a thing
        }

        GoToPos(playerPos);
        Target = playerPos;

    }
   
    // Update is called once per frame
    void Update()
    {






        if (EnemyMode == MovementMode.Patrolling)
        {
            //timer -=timer
            Timer = Timer - Time.deltaTime;
            SoundTimer -= Time.deltaTime;
            ///logic for if we get to the location and find a way to move
            //
            DescicionTimer -= Time.deltaTime;
            if (DescicionTimer <= 0)
            {
                GetNearSpot();
            }




            if (Timer <= 0)
            {

                FindNewPosition();
            }

            if (SoundTimer <= 0)
            {
                SoundTimer = 0.5f;
                WalkingSound();
            }
        }
        else if(EnemyMode == MovementMode.JustHerdNoise)
        {
            //we just going here o no need really
            if(this.transform.position==Target)
            {
                EnemyMode= MovementMode.Scouting;
                Debug.Log("EnemyScouting");
            }
        }

    }
}
