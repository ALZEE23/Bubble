using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dolphin : MonoBehaviour
{
    public bool isSit;
    public bool isWalk;
    public Transform player;
    public float followSpeed = 0.5f;
    public float detectionRadius = 10f;
    public float stoppingDistance = 1f; 
    public LayerMask obstacleMask;
    public float floatHeight = 0f; 
    public Animator animator;
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
            
            if (!IsPlayerBehindObstacle() && isSit == true)
            {
                animator.SetBool("isWalking", true);
                agent.SetDestination(player.position);
            }

            // animator.SetBool("isWalking", true);

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Stop agent if within stopping distance
        // if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        // {
        //     agent.isStopped = true; // Stop movement
        //     animator.SetBool("jumpscare", true);
        // }
        // else
        // {
        //     agent.isStopped = false; // Resume movement if out of range
        // }



        
        Vector3 position = transform.position;
        // position.y = floatHeight; // Keep height stable
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
