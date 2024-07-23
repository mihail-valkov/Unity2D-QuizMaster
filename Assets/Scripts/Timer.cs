using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
//imer

// What state is the game in?

// -answering question or showing answer
//  -Has the timer run down?

// -Change the fill amount of the timer image

    float timeToAnswer = 30f;
    [SerializeField] Image timerImage;

    float currentTime;
    bool isTimerRunning = false;

    void Update()
    {
        //if the timer is running
        if (isTimerRunning)
        {
            //decrease the time
            currentTime -= Time.deltaTime;
            //update the timer image fill amount
            timerImage.fillAmount = currentTime / timeToAnswer;

            //if the timer has run out
            if (currentTime <= 0)
            {
                //stop the timer
                StopTimer();
                //trigger the timer finished event
                TimerFinished!.Invoke();
            }
        }
    }

    public event OnTimerFinished TimerFinished;

    public delegate void OnTimerFinished();

    public void StartTimer(float time)
    {
        //set the current time to the time to answer
        timeToAnswer = currentTime = time;
        //set the timer image fill amount to 1
        timerImage.fillAmount = 1;
        //start the timer
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        //stop the timer
        isTimerRunning = false;
    }
    
}
