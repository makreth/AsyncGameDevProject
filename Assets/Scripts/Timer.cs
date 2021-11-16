using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timeRemainingMs = 642000; // 10m 42s

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTimer", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeRemainingMs > 0)
        {
            timeRemainingMs -= Time.deltaTime * 1000;
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        float minutesRemaining      = Mathf.Floor(timeRemainingMs / 1000 / 60);
        float secondsRemaining      = timeRemainingMs / 1000 % 60;
        float millisecondsRemaining = timeRemainingMs % 1000;
        timerText.text = string.Format("{0}:{1:00}:{2:000}", minutesRemaining, secondsRemaining, millisecondsRemaining);
    }
}
