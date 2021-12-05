using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Checkpoint : MonoBehaviour
{

	public GameObject spawnpoint;
	public GameObject trigger;
	public GameObject camerapoint;

	public Affectable backtrackBlocker = null;

	public Affectable lockedDoor = null;

	[SerializeField]
	private bool slideCamera = false;

	[SerializeField]
	private int requiredKeys = 0;

	private AudioManager audioManager;

	void Start()
	{
		spawnpoint.GetComponent<SpriteRenderer>().enabled = false;
		trigger.GetComponent<SpriteRenderer>().enabled = false;
		camerapoint.GetComponent<SpriteRenderer>().enabled = false;
		camerapoint.SetActive(false);
		if (lockedDoor != null)
		{
			Assert.IsTrue(requiredKeys > 0);
		}

		audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	public void SpawnPlayer(Player player)
	{
		AudioManager.instance.Play("Respawn");
		player.gameObject.transform.position = spawnpoint.transform.position;
		player.reset();
	}

	public void SetupLevel()
	{
		if (backtrackBlocker != null)
		{
			audioManager.Play("Close Door");
			backtrackBlocker.Trigger();
		}
		if (slideCamera)
		{
			camerapoint.GetComponent<CameraSlider>().startSlide();
		}
		else
		{
			Camera.main.transform.position = camerapoint.transform.position;
		}
	}

	public int GetRequiredKeys()
	{
		return requiredKeys;
	}
}
