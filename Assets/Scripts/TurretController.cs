using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum FirePattern{
    Single,
    Blast
}

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
    private FirePattern firePattern;
    [SerializeField]
    [Range(0f,10f)]
    private float rotationSpeed;
    [SerializeField]
    private float angleSpread;
    private float m_delta_rotation;
    [SerializeField]
    private RotationDirection startingRotationDirection = RotationDirection.Clockwise;
    private RotationDirection m_rotation_dir = RotationDirection.Clockwise;
    private float m_tick_timer;
    private float m_interval_timer;
    private bool firing;


    void Start()
    {   
        m_rotation_dir = startingRotationDirection;
        m_tick_timer = 0;
        firing = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
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
        

        m_tick_timer += 1;
        if(firing && m_tick_timer > fixedTicksPerShot){
            m_tick_timer = 0;
            if(firePattern == FirePattern.Single){
                ProjectileBehavior clone_bullet = Instantiate(bullet, transform.position + (transform.up * 0.5f), transform.rotation).GetComponent<ProjectileBehavior>();
                clone_bullet.setSpeed(projectileSpeed);
                clone_bullet.applyProjectileType(bulletType);
            }
        }

        m_interval_timer += 1;
        if(m_interval_timer > fixedTicksPerInterval && fixedTicksPerInterval > 0){
            m_interval_timer = 0;
            firing = !firing;
        }
    }

    void OnBecameInvisible(){
        gameObject.SetActive(false);
    }

    void OnBecameVisible(){
        gameObject.SetActive(true);
    }
}
