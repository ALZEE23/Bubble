using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    // Start is called before the first frame update
    public int trash;
    public int rak;
    public int lumbas;
    public int toy;
    public int bin;
    public int book;
    public bool isSleep;
    private bool isInTrigger = false;
    private GameObject currentObject;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI textButton;
    public Animator animator;
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

            if (currentObject != null && currentObject.tag == "sofa")
            {
                Debug.Log("Sleep");
                textMeshPro.text = "Sleep";
                animator.SetBool("Sleep", true);
                currentObject = null;
                isInTrigger = false;
            }

            if (currentObject != null && currentObject.tag == "box")
            {
                Debug.Log("Box");
                textMeshPro.text = "WTF is this?";
                currentObject = null;
                isInTrigger = false;
            }
        }

        if (bin > 12 && rak == 6 || bin == 14 && rak == 6)
        {
            textMeshPro.text = "Go Sleep";
            isSleep = true;
        }



    }

    public void Next()
    {
        SceneManager.LoadScene("Stage 2");
    }

    public void OnTriggerStay(Collider other)
    {
        if (isSleep == false)
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

            if (other.gameObject.tag == "book")
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
                textMeshPro.text = "Ayah, mana barang barangku yang kau janjikan itu huh? disaat diriku sudah seperti ini kau pergi begitu saja.";
                isInTrigger = true;
                currentObject = other.gameObject;
                textButton.enabled = true;
                textButton.text = "[LMB]";
                StartCoroutine(Walking(other.gameObject));
            }

            if(other.gameObject.tag == "box")
            {
                textMeshPro.text = "Open the box";
                isInTrigger = true;
                currentObject = other.gameObject;
                textButton.enabled = true;
                textButton.text = "[LMB]";
            }

        }
        else
        {
            if (other.gameObject.tag == "sofa")
            {
                textMeshPro.text = "Sleep";
                isInTrigger = true;
                currentObject = other.gameObject;
                textButton.enabled = true;
                textButton.text = "[LMB]";
            }
        }



    }

    IEnumerator Walking(GameObject other)
    {
        yield return new WaitForSeconds(8);
        Dolphin dolphin = other.gameObject.GetComponent<Dolphin>();
        dolphin.isWalk = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentObject)
        {
            isInTrigger = false;
            currentObject = null;
            textButton.enabled = false;
            textMeshPro.text = "";
        }
    }
}
