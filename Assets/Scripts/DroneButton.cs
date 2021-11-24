using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneButton : Button
{
	// Trigger the effect in all specified objects when pressed
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player")){
			ActivateButton();
		}
	}
}
