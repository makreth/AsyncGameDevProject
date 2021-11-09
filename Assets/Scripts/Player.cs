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
    private float jumpVelocity;
    private float gravity;
    private Vector3 velocity;
    private DroneController droneController;
    void Start(){
        droneController = GetComponent<DroneController>();
        jumpVelocity = (2 * jumpHeight * moveSpeed) / distanceToPeak;
        gravity = (-2 * jumpHeight * (float) Math.Pow(moveSpeed,2)) / (float) Math.Pow(distanceToPeak, 2);
    }

    void FixedUpdate(){
        if(droneController.collisions.above || droneController.collisions.below){
            velocity.y = 0;
        }

        Vector2 move_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
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
}
