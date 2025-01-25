using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // Start is called before the first frame update
    public int trash;
    public int toy;
    public int bin;
    private bool isInTrigger = false;
    private GameObject currentObject;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI textButton;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInTrigger && Input.GetMouseButton(0))
        {
            // textMeshPro.enabled = true;
            if (currentObject != null && currentObject.tag == "trash")
            {
                Debug.Log("Trash");
                textMeshPro.text = "Go to the bin";
                trash++;
                Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "toy")
            {
                Debug.Log("Toy");
                textMeshPro.text = "Give to Lumbas";
                toy++;
                Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "bin")
            {
                Debug.Log("Bin");
                textMeshPro.text = "Bin";
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
            textMeshPro.text = "Pick up trash";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
        }

        if (other.gameObject.tag == "toy")
        {
            textMeshPro.text = "Pick up toy";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
        }

        if (other.gameObject.tag == "bin")
        {
            textMeshPro.text = "Throw trash";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
        }

        if (other.gameObject.tag == "bubble")
        {
            textMeshPro.text = "Blow bubble";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[RMB]";
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
