using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public bool onSwitch;
    public bool lightStatus;
    public GameObject theLight;

    void OnTriggerEnter(Collider other)
    {
        onSwitch = true;
    }

    void OnTriggerExit(Collider other)
    {
        onSwitch = false;
    }

    void Update()
    {
        if (theLight.active == true)
        {
            lightStatus = true;
        }
        else
        {
            lightStatus = false;
        }

        if (onSwitch)
        {
            if (lightStatus)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    theLight.active = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    theLight.active = true;
                }
            }
        }
        
        Game.Player.SetPlayerSpeedFactor(2f, 2f);
    }

    void OnGUI()
    {
        if (onSwitch)
        {
            if (lightStatus)
            {
                GUI.Box(new Rect(0, 0, 200, 20), "Press E to close the light");

            }
            else
            {
                GUI.Box(new Rect(0, 0, 200, 20), "Press E to open the light");
            }
        }
    }
}
