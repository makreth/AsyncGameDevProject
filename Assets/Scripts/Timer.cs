using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timerValueMs = 0f; // 10m 42s

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTimer", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timerValueMs += Time.deltaTime * 1000;
        UpdateTimer();
    }

    void UpdateTimer()
    {
        float minutesRemaining      = Mathf.Floor(timerValueMs / 1000 / 60);
        float secondsRemaining      = timerValueMs / 1000 % 60;
        float millisecondsRemaining = timerValueMs % 1000;
        timerText.text = string.Format("{0}:{1:00}:{2:000}", minutesRemaining, secondsRemaining, millisecondsRemaining);
    }
}
