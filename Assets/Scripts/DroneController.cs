using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (BoxCollider2D))]
public class DroneController : MonoBehaviour
{
    public LayerMask collisionMask;
    const float SKIN_WIDTH = .015f;
    [SerializeField]
    [Range(2,6)]
    private int horizontalRayCount = 4;
    [SerializeField]
    [Range(2,6)]
    private int verticalRayCount = 4;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;
    private BoxCollider2D m_collider;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;
    
    void Start(){
        m_collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity){
        UpdateRaycastOrigins();
        collisions.Reset();
        if(velocity.x != 0){
            HorizontalCollisions(ref velocity);
        }
        if(velocity.y != 0){
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
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

    void UpdateRaycastOrigins(){
        Bounds bounds = m_collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing(){
        Bounds bounds = m_collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
    
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private struct RaycastOrigins{
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo{
        public bool above, below;
        public bool left, right;

        public void Reset(){
            above = below = false;
            left = right = false;
        }
    }
}
