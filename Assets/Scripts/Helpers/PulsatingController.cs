using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsatingController : MonoBehaviour
{

    [SerializeField] private Material pulseMaterial;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseAmount = 0;

    private bool increasePulse;
    private bool canPulse;


    public void Start()
    {
        StartCoroutine(PulseOverTime());
    }

    private void OnDestroy()
    {
        pulseMaterial.color = new Color(pulseMaterial.color.r, pulseMaterial.color.g, pulseMaterial.color.b, 0f);
    }

    private void Update()
    {
        if (canPulse)
        {
            if (increasePulse)
            {
                pulseAmount += pulseSpeed * Time.deltaTime;
                if (pulseAmount >= .35f)
                    increasePulse = false;
            }
            else
            {
                pulseAmount -= pulseSpeed * (Time.deltaTime);
                if (pulseAmount <= 0)
                {
                    pulseAmount = 0;
                    increasePulse = true;
                }
            }
        }
        else
        {
            pulseAmount -= pulseSpeed * (Time.deltaTime);
            if (pulseAmount <= 0)
            {
                pulseAmount = 0;
                increasePulse = true;
            }
        }
        
        pulseMaterial.color = new Color(pulseMaterial.color.r, pulseMaterial.color.g, pulseMaterial.color.b, pulseAmount);
    }

    public void IncreasePulse()
    {
        canPulse = true;
    }

    public void StopPulse()
    {
        canPulse = false;
    }

    private IEnumerator PulseOverTime()
    {
        while (true)
        {
            IncreasePulse();
            yield return new WaitForSeconds(5f);
            StopPulse();
            yield return new WaitForSeconds(5f);
        }
    }
}
