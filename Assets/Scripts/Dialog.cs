using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public Chat[] chats;
    public TextMeshPro textMeshPro;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        chats[0].messages[0].isDisplayed = true;
    }
}
