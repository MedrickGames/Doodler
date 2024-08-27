using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nose : MonoBehaviour
{
    private Camera _cam;
    public Booger booger;
    public Transform spawnPoint;

   
    void Start()
    {
        _cam = Camera.main;
    }

   
    void Update()
    {
       

        if (Input.GetMouseButtonDown(0))
        {
            var mousePosWorld = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane));
        
            // Calculate the direction from tank position to mouse position
            Vector3 aimDirection = mousePosWorld - transform.position;
            aimDirection.z = 0f; // Keep it in the 2D plane

            // Rotate the tank to face the mouse
            transform.up = aimDirection.normalized;

            Instantiate(booger, spawnPoint.position, Quaternion.identity).Init(transform.up);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Skin Change");
        }
    }
}