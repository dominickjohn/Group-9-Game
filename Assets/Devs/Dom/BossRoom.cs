using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Game.GetGameManager().BossBattle();
        GetComponent<Boss>().healthbar.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        CloseDown();
    }

    public GameObject bossDoor;

    public void CloseDown()
    {
        bossDoor.SetActive(false);
        if(GetComponent<Boss>())
            GetComponent<Boss>().healthbar.gameObject.SetActive(false);
        Game.GetGameManager().WinGame();
    }
    private void OnDisable()
    {
        CloseDown();
    }
}
