using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileBehavior : MonoBehaviour
{
    private float speed = 0f;
    // Start is called before the first frame update
    private SpriteRenderer m_SpriteRenderer;
    private IEnumerator hitStopCall;
    private bool hitStopped;

    private float angle = 0f;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.red;
        
        hitStopCall = null;
        hitStopped = false;

    }
    void FixedUpdate()
    {
        float added_speed = speed;
        if(hitStopCall != null){
            if(hitStopped){
                added_speed = 0;
            }
            else{
                added_speed = added_speed / 2;
            }
            
        }
        transform.Translate( transform.up * added_speed, Space.World);
    }

    public IEnumerator HitStopProjectile(){
        m_SpriteRenderer.color = Color.white;
        hitStopped = true;
        yield return new WaitForSeconds(0.1f);
        hitStopped = false;
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        hitStopCall = null;
    }

    public void setSpeed(float new_speed){
        speed = new_speed;
    }

    public void setAngle(float new_angle){
        angle = new_angle;
    }

    public void setHitStopCall(IEnumerator call){
        hitStopCall = call;
    }

    public IEnumerator getHitStopCall(){
        return hitStopCall;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
