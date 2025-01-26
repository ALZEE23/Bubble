using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public GameObject blow;
    private Vector3 velocity;
    private bool isGrounded;
    public bool isBlow = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject deer;
    public Camera mainCamera;
    public Camera bubbleCamera;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);




        if (Input.GetMouseButton(1))
        {
            isBlow = true;
            blow.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            BoxCollider blowCollider = blow.GetComponent<BoxCollider>();
            if (blowCollider != null)
            {
                blowCollider.size = blow.transform.localScale;
            }
        }
        else
        {
            isBlow = false;
            StartCoroutine(Shrink());
        }

        IEnumerator Shrink()
        {
            yield return new WaitForSeconds(0.5f);
            if (isBlow == false)
            {
                blow.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bubble")
        {
            blow.SetActive(false);
            mainCamera.enabled = false;
            bubbleCamera.enabled = true;
            // bubbleCamera.targetDisplay = 1;
            deer.SetActive(true);
            this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "water")
        {
            blow.SetActive(false);
            mainCamera.enabled = true;
            bubbleCamera.enabled = false;
            // bubbleCamera.targetDisplay = 1;
            deer.SetActive(false);
            this.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(other.gameObject);
        }
    }


}