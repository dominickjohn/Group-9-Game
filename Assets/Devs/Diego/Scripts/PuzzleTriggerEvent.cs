using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleTriggerEvent : MonoBehaviour, Useable
{
    public UnityEvent myEvent;


    public void OnUse()
    {
        if(myEvent != null)
            myEvent.Invoke();
    }

    public void OnSelect()
    {
    }

    public void OnDeselect()
    {
    }
}
