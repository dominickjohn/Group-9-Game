using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    bool doorOpen;

    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            doorOpen = true;
            Doors("Open");
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(doorOpen)
        {
            doorOpen = false;
            Doors("Close");
        }
    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
    }
}
