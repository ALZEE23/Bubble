using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    // Start is called before the first frame update
    public int trash;
    public int rak;
    public int lumbas;
    public int toy;
    public int bin;
    public int book;
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

            if (currentObject != null && currentObject.tag == "book")
            {
                Debug.Log("Book");
                textMeshPro.text = "Put on the shelf";
                book++;
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
                    bin += trash;
                    trash = 0;
                }
                // if (toy > 0)
                // {
                //     toy--;
                // }

                
                // Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "rak")
            {
                Debug.Log("Rak");
                textMeshPro.text = "Shelf";
                if (book > 0)
                {
                    rak += book;
                    book = 0;
                }
                
                // Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "lumbas")
            {
                Debug.Log("Lumbas");
                textMeshPro.text = "Lumbas";
                if (toy > 0)
                {
                    lumbas += toy;
                    toy = 0;
                }
                // if (trash > 0)
                // {
                //     trash--;
                // }

                // bin++;
                // Destroy(currentObject);
                currentObject = null;
                isInTrigger = false;
            }
        }

    }

    public void OnTriggerStay(Collider other)
    {

        Debug.Log("Trigger");
        if (other.gameObject.tag == "trash")
        {
            textMeshPro.text = "Pick up trash";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[LMB]";
        }

        if (other.gameObject.tag == "toy")
        {
            textMeshPro.text = "Give to Lumbas";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[LMB]";
        }

        if (other.gameObject.tag == "bin")
        {
            textMeshPro.text = "Throw trash";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[LMB]";
        }

        if (other.gameObject.tag == "bubble")
        {
            textMeshPro.text = "Blow bubble";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[RMB]";
        }

        if(other.gameObject.tag == "book")
        {
            textMeshPro.text = "Put on the shelf";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[LMB]";
        }

        if (other.gameObject.tag == "rak")
        {
            textMeshPro.text = "Take a book";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[LMB]";
        }

        if (other.gameObject.tag == "lumbas")
        {
            textMeshPro.text = "Give toy to Lumbas";
            isInTrigger = true;
            currentObject = other.gameObject;
            textButton.enabled = true;
            textButton.text = "[LMB]";
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
