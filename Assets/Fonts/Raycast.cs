using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
     public Transform rayOrigin;  
    public float rayDistance = 10f;  
    public LayerMask hitLayers;  
    public Color rayColor = Color.red;  

    public GameObject interactUI; 
    void Update()
    {
        
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, rayDistance, hitLayers))
        {
            interactUI.SetActive(true);
            Debug.Log("Raycast mengenai: " + hit.collider.name);

            
            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * hit.distance, rayColor);
        }
        else
        {
             interactUI.SetActive(false);
            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayDistance, rayColor);
        }
    }
}
