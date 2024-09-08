using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float range = 10f; // Range within which the random point is generated
    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        // Move to a random point on the NavMesh when the game starts
        MoveToRandomPoint();
    }

    // This method moves the agent to a random point within the NavMesh
    void MoveToRandomPoint()
    {
        Vector3 randomPoint = GetRandomPointOnNavMesh(transform.position, range);
        if (randomPoint != Vector3.zero)
        {
            agent.SetDestination(randomPoint);
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
        agent.SetDestination(targetPoint);
    }

}