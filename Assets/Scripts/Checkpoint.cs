using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
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
			levelManager.UpdateLastCheckpoint(gameObject);

			// Disable the box collider since the checkpoint's use is now done
			GetComponent<BoxCollider2D>().enabled = false;
		}
	}
}
