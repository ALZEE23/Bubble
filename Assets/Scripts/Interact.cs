using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // Start is called before the first frame update
    public int trash;
    public int toy;
    public int bin;
    private bool isInTrigger = false;
    private GameObject currentObject;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && Input.GetMouseButton(0))
        {
            if (currentObject != null && currentObject.tag == "trash")
            {
                Debug.Log("Trash");
                
                trash++;
                Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "toy")
            {
                Debug.Log("Toy");
                
                toy++;
                Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "bin")
            {
                Debug.Log("Bin");
                if (trash > 0)
                {
                    trash--;
                }
                // if (toy > 0)
                // {
                //     toy--;
                // }
                
                bin++;
                // Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "trash")
        {
            isInTrigger = true;
            currentObject = other.gameObject;
        }

        if (other.gameObject.tag == "toy")
        {
            isInTrigger = true;
            currentObject = other.gameObject;
        }

        if (other.gameObject.tag == "bin")
        {
            isInTrigger = true;
            currentObject = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentObject)
        {
            isInTrigger = false;
            currentObject = null;
        }
    }
}
