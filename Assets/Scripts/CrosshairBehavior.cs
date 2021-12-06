using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehavior : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private Color primaryColor;
    [SerializeField]
    private Color secondaryColor;

    private Player playerScript;
    private Image m_image;
    private bool secondaryFire;
    private bool killshot;

    private AudioManager audioManager;
    void Start(){
        m_image = GetComponent<Image>();
        m_image.color = primaryColor;
        playerScript = player.GetComponent<Player>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    void Update()
    {
        if (PauseMenu.isGamePaused || GameOver.isGameOver)
        {
            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
        if(Input.GetMouseButtonDown(0)){
            if(secondaryFire){
                if(playerScript.GetAmmo() > 0){
                    killshot = true;
                    playerScript.DecrementAmmo();
                    audioManager.Play("BigLaser");
                }
                else{
                    return;
                }
            }
            else{
                audioManager.Play("Laser");
            }
            RaycastHit2D[] rayHitList = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            foreach(RaycastHit2D rayHit in rayHitList){
                if(rayHit.collider != null){
                    if(rayHit.collider.gameObject.transform.CompareTag("ShotButton")){
                        rayHit.collider.gameObject.GetComponent<Button>().ActivateButton();
                        continue;
                    }
                    if(killshot && rayHit.collider.transform.CompareTag("Bullet")){
                        Destroy(rayHit.collider.gameObject);
                        continue;
                    }
                    else if(secondaryFire){
                        break;
                    }
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
            if(killshot){
                killshot = false;
            }      
        }

        if(Input.GetMouseButtonDown(1)){
            secondaryFire = !secondaryFire;
            if(!secondaryFire){
                m_image.color = primaryColor;
            }
            else{
                m_image.color = secondaryColor;
            }
            
        }        
    }

    public Color getSecondaryColor(){
        return secondaryColor;
    }
}
