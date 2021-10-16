using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	private Rigidbody2D rb;
	private LevelManager levelManager;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}

	void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.transform.tag == "Player")
			levelManager.LevelCompleted();
	}
}
