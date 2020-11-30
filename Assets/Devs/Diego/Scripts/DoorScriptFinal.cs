using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScriptFinal : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    bool isOpened = false;

    void OnTriggerStay(Collider col)
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (!isOpened)
            {
                isOpened = true;
                door.transform.position -= new Vector3(0, 2, 0);
            }
        }
    }
}
