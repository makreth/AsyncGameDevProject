using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : RaycastController
{
    public LayerMask passengerMask;
    public Vector3[] localWaypoints;

    [SerializeField]
    private float speed;
    [SerializeField]
    private bool cyclic;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    [Range(0,2)]
    private float easeAmount;
    [SerializeField]
    private bool triggered = false;
    [SerializeField]
    private bool untriggerWhenDone = false;
    public Vector3[] globalWaypoints;
    int fromWaypointIndex;
    float percentBetweenWaypoints;
    float nextMoveTime;
    List<PassengerMovement> passengerMovements;
    Dictionary<Transform, DroneController> passengerDictionary = new Dictionary<Transform, DroneController>();
    protected override void Start(){
        base.Start();

        globalWaypoints = new Vector3[localWaypoints.Length];
        for(int i = 0; i < localWaypoints.Length; i++){
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
        activeFlag = false;
        Vector3 velocity = CalculatePlatformMovement(true);
        transform.Translate(velocity);
    }

    // Update is called once per frame
    void FixedUpdate(){
        UpdateRaycastOrigins();
        Vector3 velocity = CalculatePlatformMovement();
        CalculatePassengerMovement(velocity);
        MovePassengers(true);
        transform.Translate(velocity);
        MovePassengers(false);
    }

    float Ease(float x){
        float a = easeAmount + 1;
        return Mathf.Pow(x,a) / (Mathf.Pow(x,a) + Mathf.Pow(1-x,a));
    }

    public override void Trigger(){
        activeFlag = !activeFlag;
    }

    Vector3 CalculatePlatformMovement(bool nullFlag = false){
        if(!nullFlag && (Time.time < nextMoveTime || (triggered && !activeFlag))){
            return Vector3.zero;
        }
        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);
        percentBetweenWaypoints += speed/distanceBetweenWaypoints;
        percentBetweenWaypoints = Mathf.Clamp01(percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(percentBetweenWaypoints);
        Vector3 newPos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if(percentBetweenWaypoints >= 1){
            percentBetweenWaypoints = 0;
            fromWaypointIndex += 1;
            
            if(fromWaypointIndex >= globalWaypoints.Length - 1){
                if(triggered && untriggerWhenDone){
                    activeFlag = false;
                    GetTriggeringObject().GetComponent<Button>().SetOffPosition();
                }
            }

            if(!cyclic){
                if(fromWaypointIndex >= globalWaypoints.Length - 1){
                    fromWaypointIndex = 0;
                    System.Array.Reverse(globalWaypoints);
                }
            }
            nextMoveTime = Time.time + waitTime;  
        }
        return newPos - transform.position;
    }

    void MovePassengers(bool beforeMovePlatform){
        foreach(PassengerMovement passenger in passengerMovements){
            if(!passengerDictionary.ContainsKey(passenger.transform)){
                passengerDictionary.Add(passenger.transform, passenger.transform.GetComponent<DroneController>());
            }
            if(passenger.moveBeforePlatform == beforeMovePlatform){
                passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
            }
        }
    }

    void CalculatePassengerMovement(Vector3 velocity){
        HashSet<Transform> moved_passengers = new HashSet<Transform>();
        passengerMovements = new List<PassengerMovement>();
        float dir_x = Mathf.Sign(velocity.x);
        float dir_y = Mathf.Sign(velocity.y);

        if(velocity.y != 0){
            float ray_length = Mathf.Abs(velocity.y) + SKIN_WIDTH;

            for(int i = 0; i < verticalRayCount; i++){
                Vector2 ray_origin = dir_y == -1 ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                ray_origin += Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit_passenger = Physics2D.Raycast(ray_origin, Vector2.up * dir_y, ray_length, passengerMask);
                if(hit_passenger){
                    if(moved_passengers.Contains(hit_passenger.transform)){
                        continue;
                    }
                    moved_passengers.Add(hit_passenger.transform);
                    float push_x = (dir_y == 1) ? velocity.x : 0;
                    float push_y = velocity.y - (hit_passenger.distance - SKIN_WIDTH) * dir_y;
                    passengerMovements.Add(new PassengerMovement(hit_passenger.transform, new Vector3(push_x, push_y), dir_y == 1, true));
                }
            }
        }

        if(velocity.x != 0){
            float ray_length = Mathf.Abs(velocity.x) + SKIN_WIDTH;
            for(int i = 0; i < horizontalRayCount; i++){
                Vector2 ray_origin = dir_x == -1 ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                ray_origin += Vector2.up * (horizontalRaySpacing * i);
                RaycastHit2D hit_passenger = Physics2D.Raycast(ray_origin, Vector2.right * dir_x, ray_length, passengerMask);
                if(hit_passenger){
                    if(moved_passengers.Contains(hit_passenger.transform)){
                        continue;
                    }
                    moved_passengers.Add(hit_passenger.transform);
                    float push_x = velocity.x - (hit_passenger.distance - SKIN_WIDTH) * dir_x;
                    float push_y = -SKIN_WIDTH;
                    hit_passenger.transform.Translate(new Vector3(push_x, push_y));
                    passengerMovements.Add(new PassengerMovement(hit_passenger.transform, new Vector3(push_x, push_y), false, true));
                }            
            }
        }

        if(dir_y == -1 || velocity.y == 0 && velocity.x != 0){
            float ray_length = SKIN_WIDTH * 2;

            for(int i = 0; i < verticalRayCount; i++){
                Vector2 ray_origin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
                RaycastHit2D hit_passenger = Physics2D.Raycast(ray_origin, Vector2.up, ray_length, passengerMask);
                if(hit_passenger){
                    if(moved_passengers.Contains(hit_passenger.transform)){
                        continue;
                    }
                    moved_passengers.Add(hit_passenger.transform);
                    float push_x = velocity.x;
                    float push_y = velocity.y;
                    passengerMovements.Add(new PassengerMovement(hit_passenger.transform, new Vector3(push_x, push_y), true, false));
                }
            }
        }
    }

    struct PassengerMovement{
        public Transform transform;
        public Vector3 velocity;
        public bool standingOnPlatform;
        public bool moveBeforePlatform;

        public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform){
            transform = _transform;
            velocity = _velocity;
            standingOnPlatform = _standingOnPlatform;
            moveBeforePlatform = _moveBeforePlatform;
        }
    }

    void OnDrawGizmos(){
        if(localWaypoints != null){
            Gizmos.color = Color.red;
            float size = .3f;
            for (int i = 0; i < localWaypoints.Length; i++){
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
                Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
            }
        }
    }
}
