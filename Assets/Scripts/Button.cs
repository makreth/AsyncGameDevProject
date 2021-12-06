using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    Color disabledColor;
    [SerializeField]
    bool startOn = false;
    [SerializeField]
    bool oneContactOnly = false;
    public Affectable[] triggeredObjects;

	public Sprite offSprite;
	public Sprite onSprite;

	protected SpriteRenderer m_sprite_renderer;
    protected Color originalColor;
	protected bool switchFlag;
    void Start()
    {
        m_sprite_renderer = GetComponent<SpriteRenderer>();
        originalColor = m_sprite_renderer.color;
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
            if(oneContactOnly){
                m_sprite_renderer.color = disabledColor;
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
        m_sprite_renderer.color = originalColor;
        switchFlag = false;
        m_sprite_renderer.sprite = offSprite;
    }

    public void SetOnPosition(){
        switchFlag = true;
        m_sprite_renderer.sprite = onSprite;
    }
}
