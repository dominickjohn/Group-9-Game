using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int WeapsonPrice = 1;
    public bool inTrigger;
    public bool noenoughcoin;
    public bool isPressed = false;
    public GameObject[] prefeb;
    public Sprite[] Sprite_Pic;
    private int rand;
    private int spriterand;

    private void Start()
    {
        spriterand = Random.Range(0, Sprite_Pic.Length);
        GetComponent<SpriteRenderer>().sprite = Sprite_Pic[spriterand];
    }

    private void Update()
    {
        isPressed = Input.GetKey(KeyCode.E);
    }

    void OnTriggerEnter(Collider other)
    {
        if (Game.GetGameManager().CoinCount < WeapsonPrice)
        {
            noenoughcoin = true;
        }
        inTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        noenoughcoin = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPressed == true)
        {
            if (Game.GetGameManager().CoinCount >= WeapsonPrice)
            {
                Game.GetGameManager().CoinCount -= WeapsonPrice;
                rand = spriterand;
                if (Sprite_Pic[spriterand].name == "bow_0")
                {
                    Instantiate(prefeb[rand], new Vector3(-7f, 0.6f, -6f), Quaternion.identity);
                }
                else if (Sprite_Pic[spriterand].name == "Firwand")
                {
                    Instantiate(prefeb[rand], new Vector3(-7f, 0.6f, -6f), Quaternion.identity);
                }
                else if(Sprite_Pic[spriterand].name == "Spear")
                {
                    Instantiate(prefeb[rand], new Vector3(-7f, 0.6f, -6f), Quaternion.identity);
                }
                else
                {
                    Instantiate(prefeb[rand], new Vector3(-7f, 0.6f, -6f), Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }

    void OnGUI()
    {
        if(inTrigger && noenoughcoin == false)
        {
            GUI.Button(new Rect(10, 80, 200, 20), new GUIContent("Press E to purchase the item"));
        }
        if(noenoughcoin)
        {
            GUI.Button(new Rect(10, 80, 200, 20), new GUIContent("You don't have enough coins"));
        }
    }
}
