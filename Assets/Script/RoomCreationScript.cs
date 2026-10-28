using System;
using UnityEngine;

public class RoomCreationScript : MonoBehaviour
{

    public GameObject Floor;
    public Wall[] Walls;
    public GameObject cube;
   public  GameObject WallParent;
    [ContextMenu("MakeWalls")]
    public void MakeWalls()
    {
        WallParent = Instantiate(WallParent,Floor.transform);
        WallParent.transform.position=Floor.transform.position;
        WallParent.name=Floor.name+" Walls";
        for (int i = 0; i < Walls.Length; i++)
        {
           //check for door

            //if door then start making alot of walls

            //if not then just make the thign simple



        }
    }
}

[Serializable]
public class Wall
{
    public Texture WallTexture;
   
    public GameObject Door;
    [Range(0, 100)]
    public int DoorPosition;
    public bool HasDoor;
    public Texture DoorTexture;

}
