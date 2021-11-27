using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum RotationDirection{
    Clockwise,
    CounterClockwise
}


public class TurretController : MonoBehaviour
{
    [SerializeField]
    public ProjectileType bulletType;
    [SerializeField]
    public GameObject bullet;
    [SerializeField]
    private int fixedTicksPerShot;
    [SerializeField]
    private int fixedTicksPerInterval;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    [Range(0f,10f)]
    private float rotationSpeed;
    [SerializeField]
    private float angleSpread;
    private float m_delta_rotation;
    [SerializeField]
    private RotationDirection startingRotationDirection = RotationDirection.Clockwise;
    [SerializeField]
    private int initialDelayTicks;
    private int delayTimer;
    private RotationDirection m_rotation_dir = RotationDirection.Clockwise;
    private int m_tick_timer;
    private int m_interval_timer;
    private bool firing;


    void Start()
    {   
        m_rotation_dir = startingRotationDirection;
        m_tick_timer = fixedTicksPerShot;
        m_interval_timer = 0;
        firing = true;
        delayTimer = initialDelayTicks;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(delayTimer > 0){
            delayTimer -= 1;
            return;
        }
        m_delta_rotation += rotationSpeed;
        
        if(m_delta_rotation >= angleSpread && angleSpread > 0){
            if(m_rotation_dir == RotationDirection.Clockwise){
                m_rotation_dir = RotationDirection.CounterClockwise;
            }
            else{
                m_rotation_dir = RotationDirection.Clockwise;
            }
            m_delta_rotation = 0f;
        }

        if(m_rotation_dir == RotationDirection.CounterClockwise){
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else{
            transform.Rotate(-1 * Vector3.forward * rotationSpeed);
        }
        
        m_interval_timer += 1;
        if(m_interval_timer >= fixedTicksPerInterval && fixedTicksPerInterval > 0){
            m_interval_timer = 0;
            firing = !firing;
        }
        

        m_tick_timer += 1;
        if(m_tick_timer >= fixedTicksPerShot){
            m_tick_timer = 0;
            if(firing){
                ProjectileBehavior clone_bullet = Instantiate(bullet, transform.position + (transform.up * 0.5f), transform.rotation).GetComponent<ProjectileBehavior>();
                clone_bullet.setSpeed(projectileSpeed);
                clone_bullet.applyProjectileType(bulletType);
            }
        }        
    }

    void OnBecameInvisible(){
        gameObject.SetActive(false);
    }

    void OnBecameVisible(){
        gameObject.SetActive(true);
    }
}
