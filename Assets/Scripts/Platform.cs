using UnityEngine;

public class Platform : Affectable
{
    [Header ("Edge Points")]
    [SerializeField] private Transform leftBottomEdge;
    [SerializeField] private Transform rightTopEdge;

    [Header("Platform")]
    [SerializeField] private Transform platform;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    [SerializeField] private bool osilating;
    [SerializeField] private bool vertical;
    [SerializeField] private bool horizontal;
    [SerializeField] private bool activate;
    private Vector3 initScale;
    private bool movingLeft;
    private float vspeed;
    private float hspeed;



    private void Awake()
    {
        initScale = platform.localScale;
    }

    public override void Trigger()
	{
		activate = !activate;
	}

    private void Update()
    {
        if (activate)
        {
            if (horizontal && vertical)
            {
                if (rightTopEdge.position.x - leftBottomEdge.position.x < rightTopEdge.position.y - leftBottomEdge.position.y)
                {
                    hspeed = (rightTopEdge.position.y - leftBottomEdge.position.y) / (rightTopEdge.position.x - leftBottomEdge.position.x);
                    vspeed = 1;
                }
                else
                {
                    Debug.Log("hspeed");
                    hspeed = 1;
                    vspeed = (rightTopEdge.position.x - leftBottomEdge.position.x) / (rightTopEdge.position.y - leftBottomEdge.position.y );
                }
                Debug.Log(vspeed);
                Debug.Log(hspeed);

                if (movingLeft)
                {
                    if (platform.position.x >= leftBottomEdge.position.x)
                        MoveDiagDirection(-1 * vspeed, -1 * hspeed);
                    else if (osilating)
                        DirectionChange();
                    else
                    {
                        activate = !activate;
                        DirectionChange();
                    }
                }
                else
                {
                    if (platform.position.x <= rightTopEdge.position.x)
                        MoveDiagDirection(vspeed, hspeed);
                    else if (osilating)
                        DirectionChange();
                    else
                    {
                        activate = !activate;
                        DirectionChange();
                    }
                }
            }
            else if (horizontal)
            {
                if (movingLeft)
                {
                    if (platform.position.x >= leftBottomEdge.position.x)
                        MoveInDirection(-1);
                    else if (osilating)
                        DirectionChange();
                    else
                    {
                        activate = !activate;
                        DirectionChange();
                    }
                }
                else
                {
                    if (platform.position.x <= rightTopEdge.position.x)
                        MoveInDirection(1);
                    else if (osilating)
                        DirectionChange();
                    else
                    {
                        activate = !activate;
                        DirectionChange();
                    }
                }
            }
            else if (vertical)
            {
                if (movingLeft)
                {
                    if (platform.position.y >= leftBottomEdge.position.y)
                        MoveYDirection(-1);
                    else if (osilating)
                        DirectionChange();
                    else
                    {
                        activate = !activate;
                        DirectionChange();
                    }
                }
                else
                {
                    if (platform.position.y <= rightTopEdge.position.y)
                        MoveYDirection(1);
                    else if (osilating)
                        DirectionChange();
                    else
                    {
                        activate = !activate;
                        DirectionChange();
                    }
                }
            }
            
        }
        
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _direction * speed,
            platform.position.y, platform.position.z);
    }

    private void MoveYDirection(int _direction)
    {
        platform.position = new Vector3(platform.position.x, platform.position.y + Time.deltaTime * _direction * speed, platform.position.z);
    }

    private void MoveDiagDirection(float _directionx, float _directiony)
    {
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _directionx * speed, platform.position.y + Time.deltaTime * _directiony * speed, platform.position.z);
    }
}