using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneButton : MonoBehaviour
{
	public Affectable[] triggeredObjects;

	public Sprite offSprite;
	public Sprite onSprite;

	private SpriteRenderer m_sprite_renderer;
	private bool switchFlag;

	void Start()
	{
		m_sprite_renderer = GetComponent<SpriteRenderer>();
		m_sprite_renderer.sprite = offSprite;
		switchFlag = false;
	}

	void Update()
	{

	}

	// Trigger the effect in all specified objects when pressed
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player")){
			switchFlag = !switchFlag;
			if(switchFlag){
				m_sprite_renderer.sprite = onSprite;
			}
			else{
				m_sprite_renderer.sprite = offSprite;
			}
			foreach (Affectable obj in triggeredObjects)
			{
				obj.Trigger();
			}
		}
	}
}
