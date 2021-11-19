using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance; // make it a singleton

	private bool levelComplete = false;
	public string playerPrefabName = "Player";

	private GameObject lastCheckpoint;

	private Dictionary<Ammo.AmmoType, int> ammoCounts;

	void Awake()
	{
		// handle duplicate levelmanager
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}

		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
	}

	public void LevelCompleted()
	{
		levelComplete = true;
		Time.timeScale = 0f;
		Debug.Log("Level Complete");
	}

	public void SpawnPlayer(Vector2 spawnLoc)
	{
		Instantiate(Resources.Load("Player"), spawnLoc, Quaternion.identity);
	}

	public void UpdateLastCheckpoint(GameObject newCheckpoint)
	{
		// Make sure the new checkpoint comes after the current one (aka, make sure one wasn't skipped and later hit)
		// NOTE: assumes subsequent checkpoints always are to the right
		if (!lastCheckpoint ||
				(newCheckpoint.transform.position.x > lastCheckpoint.transform.position.x))
			lastCheckpoint = newCheckpoint;
		Debug.Log(lastCheckpoint.transform.position);
	}

	public Vector3 GetCheckpointPosition()
	{
		// TODO: spawn point should be a checkpoint
		return lastCheckpoint.transform.position;
	}

	public void ShiftAmmoCount(Ammo.AmmoType ammoType, int shiftAmount)
	{
		ammoCounts[ammoType] = GetAmmoCount(ammoType) + shiftAmount;
	}

	public int GetAmmoCount(Ammo.AmmoType ammoType)
	{
		// Defaults to 0
		int ammoCount;
		ammoCounts.TryGetValue(ammoType, out ammoCount);
		return ammoCount;
	}
}
