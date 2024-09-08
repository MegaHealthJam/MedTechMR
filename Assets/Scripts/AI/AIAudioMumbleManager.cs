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
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= maxDistance)
            {
                float normalizedDistance = distance / maxDistance;
                float volume = Mathf.Lerp(minVolume, maxVolume, normalizedDistance);
                _audioSource.volume = volume;
            }
            else
            {
                _audioSource.volume = minVolume;
            }
        }
    }
}
