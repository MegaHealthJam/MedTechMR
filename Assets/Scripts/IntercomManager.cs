using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercomManager : MonoBehaviour
{

    public static IntercomManager instance;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    
    
    public void FireCodeBlue()
    {
        audioSource.Play();
    }

   
}
