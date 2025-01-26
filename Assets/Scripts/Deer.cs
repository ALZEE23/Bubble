using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Deer : MonoBehaviour
{
    public AudioClip soundEffect;
    public AudioClip soundEffectJumpscare;
    public AudioMixer audioMixer;
    public string volumeParameter = "SFXVolume";
    public Transform player;
    public float followSpeed = 0.5f;
    public float detectionRadius = 10f;
    public float stoppingDistance = 1f;
    public LayerMask obstacleMask;
    public float floatHeight = 0f;
    public Animator animator;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public Camera playerCamera;
    public Camera deerCamera;
    public Camera minorCamera;
    public bool isJumpscare = false;
    private float jumpscareTimer = 0f;
    private bool isGameOver = false;

    // private float volume = 100f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        agent.speed = followSpeed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.stoppingDistance = stoppingDistance;
        deerCamera.enabled = false;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer <= detectionRadius)
        {

            if (!IsPlayerBehindObstacle())
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
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            playerCamera.enabled = false;
            if (isJumpscare == true)
            {
                deerCamera.enabled = true;
                jumpscareTimer += Time.deltaTime;

                if (jumpscareTimer >= 2f && !isGameOver)
                {
                    SceneManager.LoadScene("GameOver");
                    isGameOver = true;
                }
            }
            minorCamera.enabled = false;
            agent.isStopped = true;
            animator.SetBool("jumpscare", true);
            isJumpscare = true;
        }
        else
        {
            playerCamera.enabled = false;
            deerCamera.enabled = false;
            minorCamera.enabled = true;
            agent.isStopped = false;
            animator.SetBool("jumpscare", false);
            isJumpscare = false;
            jumpscareTimer = 0f; // Reset timer when jumpscare ends
        }




        Vector3 position = transform.position;

        transform.position = position;
    }

    public void PlaySoundJumpscare()
    {
        if (soundEffectJumpscare != null)
        {
            GameObject audioObject = new GameObject("TemporaryAudioSource");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            // Hubungkan AudioSource ke Audio Mixer
            audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];

            // Konfigurasi AudioSource
            audioSource.clip = soundEffectJumpscare;
            audioSource.playOnAwake = false;

            // Memutar suara
            audioSource.Play();

            // Hancurkan GameObject setelah suara selesai diputar
            Destroy(audioObject, soundEffectJumpscare.length);
        }
        else
        {
            Debug.LogWarning("SoundEffect belum diatur!");
        }
    }

    public void PlaySound()
    {
        if (soundEffect != null)
        {
            // Membuat GameObject sementara untuk AudioSource
            GameObject audioObject = new GameObject("TemporaryAudioSource");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();

            // Konfigurasi AudioSource
            audioSource.clip = soundEffect;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // Stereo (0: 2D, 1: 3D)

            // Memutar suara
            audioSource.Play();

            // Hancurkan GameObject setelah suara selesai diputar
            Destroy(audioObject, soundEffect.length);
        }
        else
        {
            Debug.LogWarning("SoundEffect belum diatur!");
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Movement movement = other.GetComponent<Movement>();
            movement.controller.enabled = false;
        }
    }
}

