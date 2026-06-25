using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Sprite fullHealth;
    [SerializeField] private Sprite voidHealth;

    [SerializeField] private Image[] hearts;

    [SerializeField] private int heartsCount;

    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthCounter();
        
    }

    void HealthCounter()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.Life)
            {
                hearts[i].sprite = fullHealth;
            }

            else
            {
                hearts[i].sprite = voidHealth;
            }


            if(i < heartsCount)
            {
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}


//if (i < player.Life)
//{
//    hearts[i].sprite = fullHealth;
//}

//else
//{
//    hearts[i].sprite = voidHealth;

//}