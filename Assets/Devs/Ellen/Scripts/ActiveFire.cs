using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFire : MonoBehaviour
{
    public GameObject fire;
    public GameObject fireone;
    public GameObject firetwo;
    public GameObject firethree;

    // Start is called before the first frame update
    void Start()
    {
        fire = GameObject.Find("Fire/Fire");
        fire = transform.GetChild(0).gameObject;
        fire.SetActive(false);

        fireone = GameObject.Find("GAMEBASE/Player/Fireone");
        fireone.SetActive(false);

        firetwo = GameObject.Find("GAMEBASE/Player/Firetwo");
        firetwo.SetActive(false);

        firethree = GameObject.Find("GAMEBASE/Player/FireThree");
        firethree.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        fire.SetActive(true);
        fireone.SetActive(true);
        firetwo.SetActive(true);
        firethree.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(firedisappear());
        StartCoroutine(ffalse());
    }

    private IEnumerator firedisappear()
    {
        yield return new WaitForSeconds(3f);
        fireone.SetActive(false);
        firetwo.SetActive(false);
        firethree.SetActive(false);
    }
    private IEnumerator ffalse()
    {
        yield return new WaitForSeconds(1.5f);
        fire.SetActive(false);
    }
}
