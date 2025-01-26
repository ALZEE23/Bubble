using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator playerCam;
    public Animator gift;
    public GameObject lumbas;
    public GameObject bubble;
    public bool isBubble;

    void Start()
    {
        playerCam.SetBool("isStart", true);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Lumbas(){
        lumbas.SetActive(true);
    }

    
}
