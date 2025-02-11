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
    public GameObject Bubble;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        agent.speed = followSpeed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.stoppingDistance = stoppingDistance;
        StartCoroutine(Walking());
        IEnumerator Walking()
        {
            yield return new WaitForSeconds(4f);
            isWalk = true;
        }
        StartCoroutine(bola());
        IEnumerator bola()
        {
            yield return new WaitForSeconds(15f);
            Bubble.SetActive(true);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer <= detectionRadius)
        {

            if (!IsPlayerBehindObstacle() && isWalk == true)
            {
                animator.SetBool("Walking", true);
                agent.SetDestination(player.position);
            }

            // animator.SetBool("isWalking", true);

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        // Stop agent if within stopping distance
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            agent.isStopped = true; // Stop movement
            animator.SetBool("Sit", true);
            StartCoroutine(Wait());
            IEnumerator Wait()
            {
                yield return new WaitForSeconds(12f);
                animator.SetBool("Talk", true);
                // Bubble.SetActive(true);
            }
        }
        else
        {
            animator.SetBool("Talk", false);
            animator.SetBool("Sit", false);
            agent.isStopped = false; // Resume movement if out of range
        }




        Vector3 position = transform.position;
        // position.y = floatHeight; // Keep height stable
        transform.position = position;
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Talk", true);
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
