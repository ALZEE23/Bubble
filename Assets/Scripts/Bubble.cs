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
        rb.isKinematic = true; 
        agent.speed = followSpeed;
        agent.updateUpAxis = false; 
        agent.updateRotation = false; 
        agent.stoppingDistance = stoppingDistance; 
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRadius)
        {
            
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
}
