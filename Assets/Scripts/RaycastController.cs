using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public abstract class RaycastController : Affectable
{
    
    protected const float SKIN_WIDTH = .015f;
    [SerializeField]
    [Range(2,6)]
    protected int horizontalRayCount = 4;
    [SerializeField]
    [Range(2,6)]
    protected int verticalRayCount = 4;

    protected float horizontalRaySpacing;
    protected float verticalRaySpacing;
    protected BoxCollider2D m_collider;
    protected RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    protected virtual void Start(){
        m_collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }
    protected void UpdateRaycastOrigins(){
        Bounds bounds = m_collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    protected void CalculateRaySpacing(){
        Bounds bounds = m_collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
    
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public virtual void Move(Vector3 velocity, bool standingOnPlatform = false){
        transform.Translate(velocity);
        if(standingOnPlatform){
            collisions.below = true;
        }
    }

    protected struct RaycastOrigins{
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
