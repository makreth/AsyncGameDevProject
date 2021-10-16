using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private bool levelComplete = false;
	public string playerPrefabName = "Player";

	void Start()
	{

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
}
