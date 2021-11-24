using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ProjectileType{
    Ghost,
    Immune,
    Normal
}

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(4,24)]
    private int standardHp = 12;
    private ProjectileType m_type = ProjectileType.Normal;
    [SerializeField]
    public LayerMask obstacleCollisionMask;

    private int hp = 0;
    private float speed = 0f;
    private SpriteRenderer m_SpriteRenderer;
    private IEnumerator hitStopCall;
    private bool hitStopped;
    private Color m_color;

    private float angle = 0f;
    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        applyProjectileType(ProjectileType.Normal);

        
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
                added_speed = added_speed * 0.25f;
            }
            
        }
        transform.Translate( transform.up * added_speed, Space.World);
    }

    public void applyProjectileType(ProjectileType type){
        m_type = type;
        if(m_type == ProjectileType.Ghost){
            m_color = Color.magenta;
            hp = standardHp / 2;
        }
        if(m_type == ProjectileType.Immune){
            m_color = Color.black;
            hp = 1;
        }
        if(m_type == ProjectileType.Normal){
            m_color = Color.red;
            hp = standardHp;
        }
        m_SpriteRenderer.color = m_color;
    }

    public IEnumerator HitStopProjectile(){
        if(m_type == ProjectileType.Immune){
            yield break;
        }
        hp -= 1;
        if(hp <= 0){
            Destroy(gameObject);
            yield break;
        }
        if(m_SpriteRenderer == null){
            yield break;
        }
        m_SpriteRenderer.color = Color.white;
        hitStopped = true;
        yield return new WaitForSeconds(0.1f);
        hitStopped = false;
        if(m_SpriteRenderer == null){
            yield break;
        }
        m_SpriteRenderer.color = m_color;
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
    
    void OnCollisionEnter2D(Collision2D col){
       if ((obstacleCollisionMask.value & (1 << col.gameObject.layer)) > 0){
           if(m_type != ProjectileType.Ghost){
               Destroy(gameObject);
           }
        }
    }
}
