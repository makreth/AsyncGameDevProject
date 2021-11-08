using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public const int maxHealth = 100;
    public int health = maxHealth;
    public bool displayOverhead = true;
    public Image overheadHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(int amount)
    {
        health -= amount;
        health = health < 0 ? 0 : health;
        health = health > maxHealth ? maxHealth : health;

        float healthAmount = health / maxHealth;
        overheadHealth.fillAmount = Mathf.Clamp(healthAmount, 0, 1f);
        if (healthAmount >= 0.7f)
        {
            overheadHealth.color = new Color(0, 255, 0, 100);
        }
        else if (healthAmount >= 0.3f)
        {
            overheadHealth.color = new Color(255, 255, 0, 100);
        }
        else
        {
            overheadHealth.color = new Color(255, 0, 0, 100);
        }

    }
}
