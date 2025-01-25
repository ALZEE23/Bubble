using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bubble : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 2f;
    public float detectionRadius = 10f;
    public LayerMask obstacleMask;
    public float floatHeight = 1.5f; // Height at which the bubble floats

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = followSpeed;
        agent.updateUpAxis = false; // Prevents the agent from being affected by the ground
        agent.updateRotation = false; // Prevents the agent from rotating
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            if (!IsPlayerBehindObstacle())
            {
                agent.SetDestination(player.position);
            }
            else
            {
                // Search for a new path if the player is behind an obstacle
                agent.SetDestination(player.position);
            }
        }

        agent.SetDestination(player.position);

        // Ensure the bubble stays at the floatHeight
        Vector3 position = transform.position;
        position.y = floatHeight;
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

    Vector3 SearchNewPath()
    {
        // Implement your pathfinding logic here
        // For simplicity, let's just return the current position of the bubble
        return transform.position;
    }
}
