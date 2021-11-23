using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (DroneController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float distanceToPeak;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int maxAmmo;
    [SerializeField]
    private int healthPickupGain;
    public LayerMask projectileMask;
    public LayerMask killBoxMask;
    public LayerMask pickupMask;

    private int hp;
    private int ammo;
    private int keys;
    private float jumpVelocity;
    private float gravity;
    private Vector3 velocity;
    private DroneController droneController;
    private Checkpoint prevCheckpoint;
    private bool inputsFrozen;
    
    void Start(){
        droneController = GetComponent<DroneController>();
        jumpVelocity = (2 * jumpHeight * moveSpeed) / distanceToPeak;
        gravity = (-2 * jumpHeight * (float) Math.Pow(moveSpeed,2)) / (float) Math.Pow(distanceToPeak, 2);
        hp = maxHp;
    }

    void FixedUpdate(){ 
        if(prevCheckpoint && prevCheckpoint.lockedDoor != null){
            if(keys == prevCheckpoint.GetRequiredKeys()){
                keys = 0;
                prevCheckpoint.lockedDoor.SetActive(false);
            }
        }

        if(droneController.collisions.above || droneController.collisions.below){
            velocity.y = 0;
        }

        Vector2 move_input = new Vector2(0,0);
        if(!inputsFrozen){
            move_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        if(move_input.y > 0 && droneController.collisions.below){
            velocity.y = jumpVelocity;
        }

        if(move_input.y <= 0 && velocity.y > 0 && !droneController.collisions.below){
            velocity.y += gravity;
        }

        if(move_input.y < 0 && velocity.y < 0 && !droneController.collisions.below){
            velocity.y += gravity;
        }
        
        velocity.x = move_input.x * moveSpeed;
        velocity.y += gravity;
        droneController.Move(velocity);
    }

    public void SetCheckpoint(Checkpoint checkpoint){
        prevCheckpoint = checkpoint;
    }

    public void reset(){
        velocity = new Vector3(0,0,0);
    }

    public void freezeInputs(bool flag){
        inputsFrozen = flag;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        int targetVal = col.gameObject.layer;
        if (Utility.CheckLayer(projectileMask, targetVal)){
            GameObject projectile = col.gameObject;
            Destroy(projectile);
            hp -= 1;
        }
        if(Utility.CheckLayer(killBoxMask, targetVal)){
            hp -= 3;
            prevCheckpoint.SpawnPlayer(this);
        }
        if(Utility.CheckLayer(pickupMask, targetVal)){
            String obj_tag = col.gameObject.tag;
            if(obj_tag.Equals("Health")){
                hp += healthPickupGain;
                Debug.Log("Gained " + healthPickupGain + " health.");
                if(hp > maxHp){
                    hp = maxHp;
                }
            }
            if(obj_tag.Equals("Ammo")){
                ammo += 1;
            }
            if(obj_tag.Equals("Key")){
                keys += 1;
            }
            Destroy(col.gameObject);
        }
        if(hp <= 0){
            Destroy(gameObject);
        }
    }

    public int GetHp(){
        return hp;
    }

    public int GetMaxHp(){
        return maxHp;
    }

    public int GetAmmo(){
        return ammo;
    }

    public void DecrementAmmo(){
        ammo -= 1;
    }

    public int GetKeys(){
        return keys;
    }

    public void SetKeys(int keys){
        this.keys = keys;        
    }
}
