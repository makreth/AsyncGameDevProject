using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image healthbar;
    public Text healthbarText;
    public PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealthbar()
    {
        float playerHealth = player.health / player.maxHealth;
        healthbar.fillAmount = Mathf.Clamp(playerHealth, 0, 1f);
        healthbarText.text = Mathf.RoundToInt(playerHealth * 100).ToString();

        if(playerHealth >= 0.7f)
        {
            healthbar.color = new Color(0, 255, 0, 100);
        }
        else if (playerHealth >= 0.3f)
        {
            healthbar.color = new Color(255, 255, 0, 100);
        }
        else
        {
            healthbar.color = new Color(255, 0, 0, 100);
        }
    }
}
