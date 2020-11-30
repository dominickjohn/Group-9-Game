using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public bool inTrigger;
    public GameObject boss;
    public GameObject self;
    public GameObject Explo;


    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    public void Update()
    {
        if (boss.GetComponent<Boss>().currentHealth <= 0)
        {
            Explo.GetComponent<ParticleSystem>().Play();
            Destroy(self);
        }
    }
    private void OnGUI()
    {
        if (inTrigger)
        {
            if (Key_opendoor.have_key == true)
            {
                GUI.Box(new Rect(0, 60, 200, 25), "Press E to open the door");
            }
            else
            {
                GUI.Box(new Rect(0, 60, 200, 25), "You need to find a key");
            }
        }
    }
}
