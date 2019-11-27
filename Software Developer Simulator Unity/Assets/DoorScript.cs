using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool left;

    private bool open;
    public void ManipulateDoor()
    {
        open = !open;
        if (!left)
        {
            if (open)
                GetComponent<Animator>().Play("openDoorRight");
            else
                GetComponent<Animator>().Play("closeDoorRight");
        }
        else 
        {
            if (open)
                GetComponent<Animator>().Play("openDoorLeft");
            else
                GetComponent<Animator>().Play("closeDoorLeft");
        }
    }
}
