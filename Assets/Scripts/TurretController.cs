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
    public GameObject bullet;
    [SerializeField]
    [Range(0.0f, 359.0f)]
    private float angle;
    // Start is called before the first frame update
    [SerializeField]
    private int fixedTicksPerShot;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private FirePattern firePattern;
    [SerializeField]
    [Range(1f,10f)]
    private float rotationSpeed;
    [SerializeField]
    private float angleSpread;
    private float m_delta_rotation;
    private RotationDirection m_rotation_dir = RotationDirection.Clockwise;
    private float m_tick_timer;


    void Start()
    {   
        transform.eulerAngles = Vector3.forward * angle;
        m_tick_timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(m_rotation_dir == RotationDirection.Clockwise){
            angle += rotationSpeed;
        }
        else{
            angle -= rotationSpeed;
        }

        m_delta_rotation += rotationSpeed;
        

        if(angle < 0){
            angle = 360f + angle;
        }

        if(angle > 359){
            angle = angle - 360f;
        }
        
        if(m_delta_rotation >= angleSpread){
            if(m_rotation_dir == RotationDirection.Clockwise){
                m_rotation_dir = RotationDirection.CounterClockwise;
            }
            else{
                m_rotation_dir = RotationDirection.Clockwise;
            }
            m_delta_rotation = 0f;
        }

        transform.eulerAngles = Vector3.forward * angle;

        m_tick_timer += 1;
        if(m_tick_timer > fixedTicksPerShot){
            m_tick_timer = 0;
            if(firePattern == FirePattern.Single){
                ProjectileBehavior clone_bullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<ProjectileBehavior>();
                clone_bullet.setSpeed(projectileSpeed);
            }
        }
    }
}
