using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	public enum AmmoType
	{
		A,
		B,
		C
	};

	public AmmoType ammoType;
	public int shiftAmount = 30;

	private LevelManager levelManager;

	void Start()
	{
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}

	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			levelManager.ShiftAmmoCount(ammoType, shiftAmount);
			Destroy(gameObject);
		}
	}
}
