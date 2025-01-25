using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bubble : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;
    public float detectionRadius = 10f;
    public float stoppingDistance = 1f; // Distance at which the bubble stops following
    public LayerMask obstacleMask;
    public float floatHeight = 0f; // Height at which the bubble floats

    private NavMeshAgent agent;
    private Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Allow physics interaction
        agent.speed = followSpeed;
        agent.updateUpAxis = false; // Prevents the agent from being affected by the ground
        agent.updateRotation = false; // Prevents the agent from rotating
        agent.stoppingDistance = stoppingDistance; // Set stopping distance
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRadius)
        {
            // Check if the player is not behind an obstacle
            if (!IsPlayerBehindObstacle())
            {
                agent.SetDestination(player.position);
            }
        }

        // Stop agent if within stopping distance
        // if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        // {
        //     agent.isStopped = true; // Stop movement
        // }
        // else
        // {
        //     agent.isStopped = false; // Resume movement if out of range
        // }

        // Ensure the bubble stays at the specified float height
        Vector3 position = transform.position;
        position.y = floatHeight; // Keep height stable
        transform.position = position;
    }

    bool IsPlayerBehindObstacle()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.position, out hit, obstacleMask))
        {
            return hit.transform != player;
        }
        return false;
    }
}
