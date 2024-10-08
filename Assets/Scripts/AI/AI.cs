using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AI : MonoBehaviour
{
    public float range = 10f; // Range within which the random point is generated
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
        GameManager.OnMissionStarted += save_mission_room_number;
        SceneManager.sceneLoaded += on_scene_loaded;
        // Move to a random point on the NavMesh when the game starts
        MoveToRandomPoint();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= on_scene_loaded;
    }

    // This method moves the agent to a random point within the NavMesh
    void MoveToRandomPoint()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh(transform.position, range);
        if (randomPoint != Vector3.zero && agent != null)
        {
            agent?.SetDestination(randomPoint);
        }
    }

    // Get a random point on the NavMesh within a certain range
    Vector3 GetRandomPointOnNavMesh(Vector3 origin, float distance)
    {
        // Generate a random point within a sphere of given radius (distance)
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        // Try to find a point on the NavMesh near the random direction
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, distance, UnityEngine.AI.NavMesh.AllAreas))
        {
            // Return the point on the NavMesh
            return hit.position;
        }

        // Return zero vector if no valid point is found
        return Vector3.zero;
    }

    
    Vector3 GetTargetPointOnNavMesh(Vector3 origin, float distance)
    {
        // Generate a random point within a sphere of given radius (distance)
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        // Try to find a point on the NavMesh near the random direction
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, distance, UnityEngine.AI.NavMesh.AllAreas))
        {
            // Return the point on the NavMesh
            return hit.position;
        }

        // Return zero vector if no valid point is found
        return Vector3.zero;
    }
    // Optional: Keep moving to random points in Update method
    void Update()
    {
        // If the agent has reached its destination, move to another random point
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath)
        {
            MoveToRandomPoint();
        }
    }

    // used during a code blue to move to a target point 
    public void MoveToTargetPoint(Vector3 targetPoint)
    {
        if (agent == null)
            return;
        
        agent.SetDestination(targetPoint);
    }

    private int mission_room_number = 0;
    private void save_mission_room_number(int roomNumber)
    {
        mission_room_number = roomNumber;
        var rooms = GameObject.FindGameObjectsWithTag("Room");
        
        var targetRoom = rooms.FirstOrDefault(x => int.Parse(x.name) == mission_room_number);
        
        MoveToTargetPoint(GetTargetPointOnNavMesh(targetRoom.transform.localPosition, 5));
        
        
        
    }

    private void on_scene_loaded(Scene scene, LoadSceneMode mode)
    {
        agent.Warp(agent.transform.position);
    }

}