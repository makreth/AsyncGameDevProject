using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Affectable
{
	private BoxCollider2D boxCollider2D;
	void Start()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void Update()
	{

	}

	public override void Trigger()
	{
		// Switch door open status
		boxCollider2D.enabled = !boxCollider2D.enabled;
	}
}
