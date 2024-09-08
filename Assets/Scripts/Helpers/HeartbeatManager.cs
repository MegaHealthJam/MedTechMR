using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatManager : MonoBehaviour
{

    [SerializeField] private AudioClip[] heartbeatClips;
    [SerializeField] private float countdownTime = 120f;
    
    private AudioSource _audioSource;
    
    private float _currentTime;
    private int _currentClipIndex;
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = countdownTime;
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(SwitchTracks());
    }

    
    private IEnumerator SwitchTracks()
    {
        while (true)
        {
            _audioSource.clip = heartbeatClips[_currentClipIndex];
            _audioSource.Play();
            
            yield return new WaitForSeconds(countdownTime/heartbeatClips.Length);

            if (_currentClipIndex < heartbeatClips.Length - 1)
            {
                _currentClipIndex = 0;
                break;
            }

            _currentClipIndex = (_currentClipIndex + 1) % heartbeatClips.Length;
        }
    }


    public void SetHeartbeatAlarm(float timeInSeconds)
    {
        countdownTime = timeInSeconds;
        IntercomManager.instance.FireCodeBlue();
        StartCoroutine(SwitchTracks());
    }
}
