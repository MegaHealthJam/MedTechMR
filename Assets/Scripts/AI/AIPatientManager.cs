using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatientManager : MonoBehaviour
{
    [SerializeField] private GameObject patient;

    private Animator animator;
    private int enteredRoomNumber;
    private int missionRoomNumber;
    
    // Start is called before the first frame update
    void Start()
    {
      
        PlayerController.on_player_entered_room += save_entered_room_number;
        GameManager.OnMissionStarted += save_mission_room_number;
    }

    private void OnDestroy()
    {
        PlayerController.on_player_entered_room -= save_entered_room_number;
        GameManager.OnMissionStarted -= save_mission_room_number;
    }

    private void save_mission_room_number(int roomNumber)
    {
        if (missionRoomNumber == 0 && int.Parse(gameObject.name) == roomNumber)
        {
            patient.SetActive(true);
            animator = patient.GetComponent<Animator>();
            missionRoomNumber = roomNumber;
            CheckForTheatrics();
        }
    }

    private void save_entered_room_number(int roomNumber)
    {
        enteredRoomNumber = roomNumber;
        CheckForTheatrics();
    }


    private void CheckForTheatrics()
    {
        if (enteredRoomNumber == 0 || missionRoomNumber == 0)
            return;

        if (enteredRoomNumber != int.Parse(gameObject.name) || enteredRoomNumber != missionRoomNumber) 
            return;
        
        if (animator.enabled == false)
        {
            animator.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
