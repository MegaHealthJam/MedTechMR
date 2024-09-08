using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingController : MonoBehaviour
{

    [SerializeField] private Material pulseMaterial;
    
    private DateTime alarmTimer;
    private float initialTime;
    private float minAlpha = 0;
    private float maxAlpha = .3f;
    private bool alarmSet;

    private void OnDestroy()
    {
        pulseMaterial.color = new Color(pulseMaterial.color.r, pulseMaterial.color.g, pulseMaterial.color.b, 0f);
    }

    private void Update()
    {
        if (alarmSet)
        {
            TimeSpan timeLeft = alarmTimer - DateTime.Now;
            float timeRemaining = (float)timeLeft.TotalSeconds;

            if (timeRemaining <= 0)
            {
                TriggerAlarm();
            }
            else
            {
                PulseAlpha(timeRemaining);
            }
        }
    }

    public void SetAlarm(float timerInSeconds)
    {
        alarmTimer = DateTime.Now.AddMinutes(timerInSeconds/60);
        alarmSet = true;

        initialTime = timerInSeconds;
    }

    private void TriggerAlarm()
    {
        alarmSet = false;
        pulseMaterial.color = new Color(pulseMaterial.color.r, pulseMaterial.color.g, pulseMaterial.color.b, 0);
    }

    private void PulseAlpha(float timeRemaining)
    {
        float normalizedTimeRemaining = timeRemaining / initialTime;

        float pulseSpeed = Mathf.Lerp(0.5f, 5, 1 - normalizedTimeRemaining);

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * pulseSpeed) + 1) / 2);

        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        pulseMaterial.color = new Color(pulseMaterial.color.r, pulseMaterial.color.g, pulseMaterial.color.b, alpha);
    }

}
