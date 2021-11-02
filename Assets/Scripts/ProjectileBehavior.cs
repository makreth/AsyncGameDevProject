using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f,0.10f)]
    private float speed;
    private float angle;
    // Start is called before the first frame update
    private float _speed_x;
    private float _speed_y;
    private SpriteRenderer m_SpriteRenderer;
    private IEnumerator hitStopCall;
    private bool hitStopped;
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.red;

        angle = Random.Range(0f, 359f);
        _speed_x = (Mathf.Cos(angle) * speed);
        _speed_y = (Mathf.Sin(angle) * speed);
        hitStopCall = null;
        hitStopped = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float calc_x = _speed_x;
        float calc_y = _speed_y;
        if(hitStopCall != null){
            if(hitStopped){
                calc_x = 0;
                calc_y = 0;
            }
            else{
                calc_x = calc_x / 2;
                calc_y = calc_y / 2;
            }
            
        }
        transform.position = new Vector2(transform.position.x + calc_x, transform.position.y + calc_y);
    }

    public IEnumerator HitStopProjectile(){
        m_SpriteRenderer.color = Color.white;
        hitStopped = true;
        yield return new WaitForSeconds(0.1f);
        hitStopped = false;
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        hitStopCall = null;
    }

    public void setHitStopCall(IEnumerator call){
        hitStopCall = call;
    }

    public IEnumerator getHitStopCall(){
        return hitStopCall;
    }
}
