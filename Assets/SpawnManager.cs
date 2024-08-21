using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject platformPrefab; // The platform prefab to spawn
    public int numberOfPlatforms = 10; // Number of platforms to spawn initially
    public float levelWidth = 2.5f; // The width of the spawn area
    public float minY = 1.5f; // Minimum Y distance between platforms
    public float maxY = 3f; // Maximum Y distance between platforms

    private float spawnYPosition = 0f; // Starting Y position for spawning

    void Start()
    {
        // Spawn the initial set of platforms
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform()
    {
        // Generate a random X position within the level width
        float spawnXPosition = Random.Range(-levelWidth, levelWidth);

        // Generate a random Y distance for the next platform
        spawnYPosition += Random.Range(minY, maxY);

        // Instantiate the platform at the new position
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, 0f);
        Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
    }
}