using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluPlatS : MonoBehaviour
{
    public float speed = 5f; // Speed at which the platform moves
    private float screenWidth;
    private float direction = 1f; // Start moving right

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the width of the screen in world units
        float cameraHeight = Camera.main.orthographicSize * 2;
        screenWidth = cameraHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the platform
        float moveAmount = speed * direction * Time.deltaTime;
        transform.position += new Vector3(moveAmount, 0, 0);

        // Check if the platform has gone beyond the screen bounds
        if (transform.position.x > screenWidth / 2)
        {
            direction = -1f; // Change direction to left
        }
        else if (transform.position.x < -screenWidth / 2)
        {
            direction = 1f; // Change direction to right
        }
    }
}