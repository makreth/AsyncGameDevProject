using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public Healthbar healthbar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTakeDamage();
        CheckHeal();
    }

    void CheckTakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            health -= 5;
            health = health < 0 ? 0 : health;
            healthbar.UpdateHealthbar();
        }
    }

    void CheckHeal()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            health += 5;
            health = health > 100 ? 100 : health;
            healthbar.UpdateHealthbar();
        }
    }
}
