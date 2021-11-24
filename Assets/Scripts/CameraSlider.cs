using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlider : MonoBehaviour
{
    [SerializeField]
    [Range(1f,24f)]
    private float speed = 18f;

    private bool sliding;
    private Vector3 startPosition;
    private float startTime;
    private float length;
    // Update is called once per frame
    void Update()
    {
        if(sliding){
            float dist = (Time.time - startTime) * speed;
            float frac = dist / length;
            Camera.main.transform.position = Vector3.Lerp(startPosition, gameObject.transform.position, frac);
            if(Camera.main.transform.position == gameObject.transform.position){
                sliding = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void startSlide(){
        gameObject.SetActive(true);
        sliding = true;
        startPosition = Camera.main.transform.position;
        startTime = Time.time;
        length = Vector3.Distance(startPosition, gameObject.transform.position);
    }
}
