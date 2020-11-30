
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    public GameObject heartImage;

    public Sprite heartEmpty;
    public Sprite heartFull;

    public void SetDisplay(int health, int maxHealth)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        for (var i = 0; i < maxHealth; i++)
        {
            Image heart = Instantiate(heartImage, transform).GetComponent<Image>();
            
            if (i < health)
            {
                heart.sprite = heartFull;
            }
            else
            {
                heart.sprite = heartEmpty;
            }
        }
    }
}
