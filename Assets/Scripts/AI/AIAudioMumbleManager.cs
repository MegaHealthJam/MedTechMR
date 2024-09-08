using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAudioMumbleManager : MonoBehaviour
{

    private Transform target;
    private AudioSource _audioSource;

    public float maxVolume = 1;
    public float minVolume = .5f;
    public float maxDistance = 10f;
    
    
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = minVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (_audioSource && target)
        {
            
        }
    }
}
