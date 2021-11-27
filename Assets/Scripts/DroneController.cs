using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (BoxCollider2D))]
public class DroneController : RaycastController
{
    public LayerMask collisionMask;
    

    protected override void Start()
    {
        base.Start();
    }

    public override void Move(Vector3 velocity, bool standingOnPlatform = false){
        UpdateRaycastOrigins();
        collisions.Reset();
        if(velocity.x != 0){
            HorizontalCollisions(ref velocity);
        }
        if(velocity.y != 0){
            VerticalCollisions(ref velocity);
        }
        base.Move(velocity, standingOnPlatform);
    }

    void HorizontalCollisions(ref Vector3 velocity){
        float dir_x = Mathf.Sign(velocity.x);
        float ray_length = Mathf.Abs(velocity.x) + SKIN_WIDTH;

        for(int i = 0; i < horizontalRayCount; i++){
            Vector2 ray_origin = dir_x == -1 ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            ray_origin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit_obstacle = Physics2D.Raycast(ray_origin, Vector2.right * dir_x, ray_length, collisionMask);
            Debug.DrawRay(ray_origin, Vector2.right * dir_x * ray_length, Color.red);

            if(hit_obstacle){
                if(hit_obstacle.distance == 0){
                    continue;
                }
                
                velocity.x = (hit_obstacle.distance - SKIN_WIDTH) * dir_x ;
                ray_length = hit_obstacle.distance;

                collisions.left = dir_x == -1;
                collisions.right = dir_x == 1;
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity){
        float dir_y = Mathf.Sign(velocity.y);
        float ray_length = Mathf.Abs(velocity.y) + SKIN_WIDTH;

        for(int i = 0; i < verticalRayCount; i++){
            Vector2 ray_origin = dir_y == -1 ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            ray_origin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit_obstacle = Physics2D.Raycast(ray_origin, Vector2.up * dir_y, ray_length, collisionMask);
            Debug.DrawRay(ray_origin, Vector2.up * dir_y * ray_length, Color.red);

            if(hit_obstacle){
                velocity.y = (hit_obstacle.distance - SKIN_WIDTH) * dir_y ;
                ray_length = hit_obstacle.distance;

                collisions.below = dir_y == -1;
                collisions.above = dir_y == 1;
            }
        }
    }

    public override void Trigger(){
        return;
    }
}
