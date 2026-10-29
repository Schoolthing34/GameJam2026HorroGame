using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public EnemyMainMovement[] enemies;
    public Rooms[] allrooms;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public Transform GetRoom()
    {
        while(true)
        {
            int i=UnityEngine.Random.Range(0, allrooms.Length);

            if(CheckRoom(i))
            {
                return allrooms[i].room;
            }
        }
    }

    public bool  CheckRoom(int RoomNum)
    {
        if(!allrooms[RoomNum].Occupied)
        {
            allrooms[RoomNum].Occupied = true;
        }
        return allrooms[RoomNum].Occupied;


    }
   
}
[Serializable]
public class Rooms
{
    public Transform room;
    public bool Occupied = false;
}
