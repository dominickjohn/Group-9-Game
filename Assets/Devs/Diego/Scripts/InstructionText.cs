using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionText : MonoBehaviour
{
    public GameObject OpenPanel = null;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            OpenPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            OpenPanel.SetActive(false);
        }
    }

    private bool IsOpenPanelActive
    {
        get
        {
            return OpenPanel.activeInHierarchy;
        }
    }

    void Update()
    {
        if(IsOpenPanelActive)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                OpenPanel.SetActive(false);
            }
        }
    }
}
