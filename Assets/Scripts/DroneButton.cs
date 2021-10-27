using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneButton : MonoBehaviour
{
	public Affectable[] triggeredObjects;

	void Start()
	{

	}

	void Update()
	{

	}

	// Trigger the effect in all specified objects when pressed
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			foreach (Affectable obj in triggeredObjects)
			{
				obj.Trigger();
			}
		}
	}
}
