using UnityEngine;
using System.Collections.Generic;
public class SpawnManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab;        // The platform prefab to spawn
    public Transform doodler;                // Reference to the Doodler's transform
    public int initialPlatformCount = 10;    // Number of platforms to spawn initially
    public float minYSpacing = 1.5f;        // Minimum vertical spacing between platforms
    public float maxYSpacing = 3f;          // Maximum vertical spacing between platforms

    [Header("Spawning Parameters")]
    public float spawnBuffer = 10f;          // Distance ahead of the Doodler to spawn platforms
    public float cleanupThreshold = 15f;     // Distance below the Doodler to clean up platforms
    public float offSet = 0.5f;

    private float cameraWidth;               // Current camera width in world units
    private float highestY = 0f;             // Highest Y position of spawned platforms
    private List<GameObject> spawnedPlatforms = new List<GameObject>(); // List to track spawned platforms

    void Start()
    {
        // Initialize camera width
        UpdateCameraWidth();

        // Spawn initial platforms
        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Determine the Y position threshold for spawning new platforms
        float spawnThreshold = doodler.position.y + spawnBuffer;

        // Spawn platforms until the highestY reaches the spawnThreshold
        while (highestY < spawnThreshold)
        {
            SpawnPlatform();
        }

        // Optional: Clean up platforms that are far below the Doodler's current position
        //CleanupPlatforms();
    }

    /// <summary>
    /// Updates the camera's width based on its current orthographic size and aspect ratio.
    /// </summary>
    void UpdateCameraWidth()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && mainCamera.orthographic)
        {
            cameraWidth = mainCamera.orthographicSize * mainCamera.aspect - offSet;
        }
        else
        {
            Debug.LogWarning("Main Camera is missing or not orthographic. Platform spawning may not work as expected.");
            cameraWidth = 5f; // Default value to prevent errors
        }
    }

    /// <summary>
    /// Spawns a single platform at a random X position within the camera's width and a Y position above the last platform.
    /// </summary>
    void SpawnPlatform()
    {
        // Random X position within the camera's width
        float spawnX = Random.Range(-cameraWidth, cameraWidth);

        // Random Y spacing between minYSpacing and maxYSpacing
        float spawnY = highestY + Random.Range(minYSpacing, maxYSpacing);

        // Update the highestY to the new spawn position
        highestY = spawnY;

        // Instantiate the platform
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Add the new platform to the tracking list
        spawnedPlatforms.Add(newPlatform);
    }

    /// <summary>
    /// Cleans up platforms that are below the cleanup threshold relative to the Doodler's current Y position.
    /// </summary>
    // void CleanupPlatforms()
    // {
    //     // Define the Y position below which platforms should be destroyed
    //     float removeThreshold = doodler.position.y - cleanupThreshold;
    //
    //     // Iterate through the list of spawned platforms in reverse order
    //     for (int i = spawnedPlatforms.Count - 1; i >= 0; i--)
    //     {
    //         if (spawnedPlatforms[i].transform.position.y < removeThreshold)
    //         {
    //             // Destroy the platform and remove it from the list
    //             Destroy(spawnedPlatforms[i]);
    //             spawnedPlatforms.RemoveAt(i);
    //         }
    //     }
    // }
}