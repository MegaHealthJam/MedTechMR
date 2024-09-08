using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class MusicSpeedModifier : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float countDownTime = 60;
    [SerializeField] private float minPitch = 1f;
    [SerializeField] private float maxPitch = 3f;

    private float currentTime;

    private Button someButton;
    private void Start()
    {
        
        
        if(_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        currentTime = countDownTime;
    }

  

    private void Update()
    {
        DateTime target = DateTime.Now.AddMinutes(1);

        if (DateTime.Now == target)
        {
            // we hit are target time 
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            
            float normalizedTime = 1 - (currentTime / countDownTime);
            _audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedTime);
            _audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f/normalizedTime);
        }
        else
        {
            _audioSource.pitch = maxPitch;
        }
    }
}
