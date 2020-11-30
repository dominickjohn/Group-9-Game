﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSciptTrigger : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.position -= new Vector3(0, 2, 0);
    }

    public void OnUse()
    {
        OpenDoor();
    }

    public void OnSelect()
    {
    }

    public void OnDeselect()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
