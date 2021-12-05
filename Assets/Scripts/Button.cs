using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    bool startOn = false;
    [SerializeField]
    bool oneContactOnly = false;
    public Affectable[] triggeredObjects;

	public Sprite offSprite;
	public Sprite onSprite;

	protected SpriteRenderer m_sprite_renderer;
	protected bool switchFlag;
    void Start()
    {
        m_sprite_renderer = GetComponent<SpriteRenderer>();
		m_sprite_renderer.sprite = offSprite;
		foreach(Affectable obj in triggeredObjects){
			obj.SetTriggeringObject(gameObject);
		}
        switchFlag = !startOn;
        ActivateButton(true);
    }

    public void ActivateButton(bool initialize=false){
        if(!initialize && oneContactOnly && switchFlag){
            return;
        }
        if(!initialize){
            AudioManager.instance.Play("Computer");
            AudioManager.instance.Play("Open Door");
            foreach (Affectable obj in triggeredObjects)
            {
                obj.Trigger();
            }
        }
        
        if(switchFlag){
            SetOffPosition();
        }
        else{
            SetOnPosition();
        }
    }

    public void SetOffPosition(){
        switchFlag = false;
        m_sprite_renderer.sprite = offSprite;
    }

    public void SetOnPosition(){
        switchFlag = true;
        m_sprite_renderer.sprite = onSprite;
    }
}
