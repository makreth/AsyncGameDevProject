using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBehavior : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    
        if(Input.GetMouseButtonDown(0)){
            RaycastHit2D[] rayHitList = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            foreach(RaycastHit2D rayHit in rayHitList){
                if(rayHit.collider != null){
                    ProjectileBehavior script = rayHit.collider.gameObject.GetComponent<ProjectileBehavior>();
                    if(script != null){
                        if(script.getHitStopCall() != null){
                            StopCoroutine(script.getHitStopCall());
                        }
                        IEnumerator hitStopCall = script.HitStopProjectile();
                        script.setHitStopCall(hitStopCall);
                        StartCoroutine(hitStopCall);
                    }
                }
            }
            
        }
        
    }
}
