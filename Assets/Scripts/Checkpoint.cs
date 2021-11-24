using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Checkpoint : MonoBehaviour
{

	public GameObject spawnpoint;
	public GameObject trigger;
	public GameObject camerapoint;

	public GameObject backtrackBlocker = null;

	public GameObject lockedDoor = null;

	[SerializeField]
	private bool slideCamera = false;

	[SerializeField]
	private int requiredKeys = 0;

	void Start()
	{
		spawnpoint.GetComponent<SpriteRenderer>().enabled = false;
		trigger.GetComponent<SpriteRenderer>().enabled = false;
		camerapoint.GetComponent<SpriteRenderer>().enabled = false;
		camerapoint.SetActive(false);
		if(backtrackBlocker != null){
			backtrackBlocker.SetActive(false);
		}
		if(lockedDoor != null){
			Assert.IsTrue(requiredKeys > 0);
		}
	}

	public void SpawnPlayer(Player player){
		player.gameObject.transform.position = spawnpoint.transform.position;
		player.reset();
	}

	public void SetupLevel(){
		if(backtrackBlocker != null){
			backtrackBlocker.SetActive(true);
		}
		if(slideCamera){
			camerapoint.GetComponent<CameraSlider>().startSlide();
		}
		else{
			Camera.main.transform.position = camerapoint.transform.position;
		}
	}

	public int GetRequiredKeys(){
		return requiredKeys;
	}
}
