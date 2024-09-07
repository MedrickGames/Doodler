using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject greenPlatformPrefab;   // Green platform prefab
    public GameObject bluePlatformPrefab;    // Blue platform prefab (appears intermittently)
    public GameObject whitePlatformPrefab;   // White platform prefab (spawns rarely)
    public GameObject breakingPlatformPrefab;// Breaking platform prefab (avoid jumping on)
    public Transform doodler;                // Reference to the Doodler's transform
    public int initialPlatformCount = 10;    // Number of platforms to spawn initially
    public float minYSpacing = 1.5f;         // Minimum vertical spacing between platforms
    public float maxYSpacing = 3f;           // Maximum vertical spacing between platforms

    [Header("Spawning Parameters")]
    public float spawnBuffer = 10f;          // Distance ahead of the Doodler to spawn platforms
    public float offSet = 0.5f;

    [Header("Difficulty Progression")]
    public int bluePlatformMinInterval = 10; // Minimum number of green platforms before a blue one
    public int bluePlatformMaxInterval = 20; // Maximum number of green platforms before a blue one
    public float whitePlatformChance = 0.05f; // Chance for a white platform to spawn (5%)
    public float breakingPlatformChance = 0.1f; // Chance for a breaking platform to spawn between normal platforms (10%)
    public int scoreThresholdForEnemies = 200; // Score threshold to start spawning enemies
    public GameObject enemyPrefab;           // Enemy prefab to spawn
    public float enemySpawnChance = 0.1f;    // Chance to spawn an enemy (0.1 = 10%)

    [Header("Black Hole Settings")]
    public GameObject blackHolePrefab;       // Black Hole prefab
    public float blackHoleChance = 0.01f;    // Very low chance of black hole spawn (1%)
    public int blackHoleScoreThreshold = 100; // Black hole spawns after score of 100

    private float cameraWidth;               // Current camera width in world units
    private float highestY = 0f;             // Highest Y position of spawned platforms
    private List<GameObject> spawnedPlatforms = new List<GameObject>(); // List to track spawned platforms

    private int currentScore;
    private int greenPlatformCount = 0;      // Counter for the number of green platforms spawned
    private int nextBluePlatformInterval;    // Determines when the next blue platform should spawn

    public GameObject platformHolder;

    void Start()
    {
        // Initialize camera width
        UpdateCameraWidth();

        // Set initial interval for the first blue platform
        SetNextBluePlatformInterval();

        // Spawn initial platforms
        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Get current score from your game manager (you need to implement this)
        currentScore = GameObject.Find("GameManager").GetComponent<GameManager>().score;

        // Determine the Y position threshold for spawning new platforms
        float spawnThreshold = doodler.position.y + spawnBuffer;

        // Spawn platforms until the highestY reaches the spawnThreshold
        while (highestY < spawnThreshold)
        {
            SpawnPlatform();
        }
    }

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

    void SpawnPlatform()
    {
        // Random X position within the camera's width
        float spawnX = Random.Range(-cameraWidth, cameraWidth);

        // Random Y spacing between minYSpacing and maxYSpacing
        float spawnY = highestY + Random.Range(minYSpacing, maxYSpacing);

        // Update the highestY to the new spawn position
        highestY = spawnY;

        // Determine platform type
        GameObject platformPrefabToUse;

        if (greenPlatformCount >= nextBluePlatformInterval)
        {
            // Blue platform spawn
            platformPrefabToUse = bluePlatformPrefab;
            greenPlatformCount = 0;
            SetNextBluePlatformInterval();
        }
        else if (Random.value < whitePlatformChance)
        {
            // White platform spawn
            platformPrefabToUse = whitePlatformPrefab;
        }
        else
        {
            // Green platform spawn
            platformPrefabToUse = greenPlatformPrefab;
            greenPlatformCount++;
        }

        // Instantiate the platform
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject newPlatform = Instantiate(platformPrefabToUse, spawnPosition, Quaternion.identity);
        newPlatform.transform.parent = platformHolder.transform;

        // Add breaking platform in between platforms
        if (Random.value < breakingPlatformChance)
        {
            SpawnBreakingPlatform(spawnY);
        }

        // Check if we should spawn an enemy
        if (currentScore >= scoreThresholdForEnemies && Random.value < enemySpawnChance)
        {
            SpawnEnemy(spawnPosition);
        }

        // Check if we should spawn a black hole
        if (currentScore >= blackHoleScoreThreshold && Random.value < blackHoleChance)
        {
            SpawnBlackHole();
        }

        // Add the new platform to the tracking list
        spawnedPlatforms.Add(newPlatform);
    }

    void SetNextBluePlatformInterval()
    {
        // Randomly determine the next interval for blue platform spawning
        nextBluePlatformInterval = Random.Range(bluePlatformMinInterval, bluePlatformMaxInterval);
    }

    void SpawnBreakingPlatform(float aboveY)
    {
        // Random X position for the breaking platform
        float spawnX = Random.Range(-cameraWidth, cameraWidth);

        // Slightly below the current platform's Y position
        float spawnY = aboveY - Random.Range(1f, 1.5f);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        GameObject breakingPlatform = Instantiate(breakingPlatformPrefab, spawnPosition, Quaternion.identity);
        breakingPlatform.transform.parent = platformHolder.transform;
    }

    void SpawnEnemy(Vector3 platformPosition)
    {
        float enemySpawnX = Random.Range(-cameraWidth, cameraWidth);
        Vector3 enemySpawnPosition = new Vector3(enemySpawnX, platformPosition.y + Random.Range(1f, 2f), 0f);
        Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
    }

    void SpawnBlackHole()
    {
        // Randomize black hole position (far left or right)
        float blackHoleX = Random.value > 0.5f ? -cameraWidth : cameraWidth;
        Vector3 blackHolePosition = new Vector3(blackHoleX, highestY + Random.Range(1f, 2f), 0f);
        Instantiate(blackHolePrefab, blackHolePosition, Quaternion.identity);

        // Spawn platform in the opposite direction
        float platformX = blackHoleX > 0 ? -cameraWidth : cameraWidth;
        Vector3 platformPosition = new Vector3(platformX, blackHolePosition.y, 0f);
        GameObject escapePlatform = Instantiate(greenPlatformPrefab, platformPosition, Quaternion.identity);
        escapePlatform.transform.parent = platformHolder.transform;
    }
}
