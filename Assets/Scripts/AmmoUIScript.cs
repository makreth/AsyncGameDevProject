using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class AmmoUIScript : MonoBehaviour
{
    public Player player;
    private Text ammoText;
    
    void Start()
    {
        ammoText = GetComponent<Text>();
        ammoText.color = transform.parent.GetComponent<CrosshairBehavior>().getSecondaryColor();
    }

    public void UpdateAmmo(){
        ammoText.text = player.GetAmmo().ToString();
    }
}
