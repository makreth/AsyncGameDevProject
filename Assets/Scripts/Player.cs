using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DroneController))]
public class Player : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float jumpHeight;
	[SerializeField]
	private float distanceToPeak;
	[SerializeField]
	private int maxHp;
	[SerializeField]
	private int maxAmmo;
	[SerializeField]
	private int healthPickupGain;
	[SerializeField]
	bool invincible = false;
	public LayerMask projectileMask;
	public LayerMask killBoxMask;
	public LayerMask pickupMask;
	public Healthbar health;
	public AmmoUIScript ammoDisplay;
	private int hp;
	private int ammo;
	private int keys;
	private float jumpVelocity;
	private float gravity;
	private Vector3 velocity;
	private DroneController droneController;
	private Checkpoint prevCheckpoint;
	private bool inputsFrozen;
	private SpriteRenderer m_SpriteRenderer;

	private AudioManager audioManager;

	void Start()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		droneController = GetComponent<DroneController>();
		jumpVelocity = (2 * jumpHeight * moveSpeed) / distanceToPeak;
		gravity = (-2 * jumpHeight * (float)Math.Pow(moveSpeed, 2)) / (float)Math.Pow(distanceToPeak, 2);
		hp = maxHp;

		audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
	}

	void FixedUpdate()
	{
		if (droneController.collisions.above || droneController.collisions.below)
		{
			velocity.y = 0;
		}

		Vector2 move_input = new Vector2(0, 0);
		if (!inputsFrozen)
		{
			move_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}

		if (move_input.y > 0 && droneController.collisions.below)
		{
			velocity.y = jumpVelocity;
		}

		if (move_input.y <= 0 && velocity.y != 0 && !droneController.collisions.below)
		{
			velocity.y += gravity;
		}

		if (move_input.y < 0 && velocity.y < 0 && !droneController.collisions.below)
		{
			velocity.y += gravity * 2;
		}

		if (move_input.x < 0 && droneController.collisions.below)
		{
			m_SpriteRenderer.flipX = true;
		}
		if (move_input.x > 0 && droneController.collisions.below)
		{
			m_SpriteRenderer.flipX = false;
		}

		// Movement sounds
		if (move_input.x == 0)
			audioManager.Pause("Drone Move", true);
		else
			audioManager.Pause("Drone Move", false);

		velocity.x = move_input.x * moveSpeed;
		velocity.y += gravity;
		droneController.Move(velocity);
	}
	public void SetCheckpoint(Checkpoint checkpoint)
	{
		prevCheckpoint = checkpoint;
	}

	public void reset()
	{
		velocity = new Vector3(0, 0, 0);
	}

	public void freezeInputs(bool flag)
	{
		inputsFrozen = flag;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		int targetVal = col.gameObject.layer;
		if (Utility.CheckLayer(projectileMask, targetVal))
		{
			GameObject projectile = col.gameObject;
			Destroy(projectile);

			audioManager.Play("Damage");

			if (!invincible)
			{
				hp -= 1;
				health.UpdateHealthbar();
				StartCoroutine(Flicker());
			}

		}
		if (Utility.CheckLayer(killBoxMask, targetVal))
		{
			audioManager.Play("Damage");

			if (!invincible)
			{
				hp -= 3;
				health.UpdateHealthbar();
			}

			prevCheckpoint.SpawnPlayer(this);
		}
		if (Utility.CheckLayer(pickupMask, targetVal))
		{
			String obj_tag = col.gameObject.tag;
			if (obj_tag.Equals("Health"))
			{
				audioManager.Play("Pickup");

				hp += healthPickupGain;
				if (hp > maxHp)
				{
					hp = maxHp;
				}
				health.UpdateHealthbar();
			}
			if (obj_tag.Equals("Ammo"))
			{
				audioManager.Play("Pickup");

				ammo += 1;
				ammoDisplay.UpdateAmmo();
			}
			if (obj_tag.Equals("Key"))
			{
				audioManager.Play("Pickup");

				keys += 1;
				if (prevCheckpoint && prevCheckpoint.lockedDoor != null)
				{
					if (keys == prevCheckpoint.GetRequiredKeys())
					{
						keys = 0;
						prevCheckpoint.lockedDoor.Trigger();
						audioManager.Play("Open Door");
					}
				}
			}
			Destroy(col.gameObject);
		}
		if (hp <= 0)
		{
			Destroy(gameObject);
		}
	}

	public int GetHp()
	{
		return hp;
	}

	public int GetMaxHp()
	{
		return maxHp;
	}

	public int GetAmmo()
	{
		return ammo;
	}

	public void DecrementAmmo()
	{
		ammo -= 1;
		ammoDisplay.UpdateAmmo();
	}

	public int GetKeys()
	{
		return keys;
	}

	public void SetKeys(int keys)
	{
		this.keys = keys;
	}

	private IEnumerator Flicker()
	{
		m_SpriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		m_SpriteRenderer.color = Color.grey;
	}
}
