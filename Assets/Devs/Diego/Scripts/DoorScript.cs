using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public static bool doorKey;
    public bool open;
    public bool close;
    public bool inTrigger;

    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    void Update()
    {
        if (inTrigger)
        {
            if (close)
            {
                if (Game.GetGameManager().KeyCount > 0)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        open = true;
                        Game.GetGameManager().KeyCount--;
                        Destroy(gameObject);
                        close = false;
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    close = true;
                    open = false;
                }
            }
        }

        if (open)
        {
            transform.position -= new Vector3(0, 2, 0);
        }
    }

    void OnGUI()
    {
        if (inTrigger)
        {
            if (open)
            {
                GUI.Box(new Rect(0, 0, 200, 25), "Press E to close");
            }
            else
            {
                if (Game.GetGameManager().KeyCount > 0)
                {
                    GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 200, 25), "Press E to open");
                }
                else
                {
                    GUI.Box(new Rect(Screen.width * 0.5f, Screen.height * 0.5f, 200, 25), "Need a key!");
                }
            }
        }
    }
}
