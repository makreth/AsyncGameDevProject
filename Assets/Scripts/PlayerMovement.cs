using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	// Buttons
	public KeyCode leftKey = KeyCode.LeftArrow;
	public KeyCode rightKey = KeyCode.RightArrow;
	public KeyCode jumpKey = KeyCode.Space;
	public float jumpHeight;
	public float moveSpeed;

	private Rigidbody2D rb;
	private BoxCollider2D boxCollider;

	private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Vector2 vel = rb.velocity;

		// Jumping
		if (Input.GetKeyDown(jumpKey) && isGrounded)
		{
			vel.y = jumpHeight;
		}

		if (Input.GetKey(leftKey))
		{
			vel.x = -moveSpeed;
		}
		else if (Input.GetKey(rightKey))
		{
			vel.x = moveSpeed;
		}
		else
		{
			vel.x = 0;
		}

		rb.velocity = vel;

		// Reload
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		isGrounded = true;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		isGrounded = false;
	}
}
