using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform doodler;
    public PlayerScript player;
    public float smoothSpeed = 0.125f; // Smooth speed for camera movement
    public float followThreshold = -0.33f; // Threshold to start following (one-third of the screen)

    private float cameraHeight;

    void Start()
    {
        cameraHeight = Camera.main.orthographicSize;
        player = GameObject.Find("Doodler").GetComponent<PlayerScript>();
    }

    void LateUpdate()
    {
        // Determine the threshold position in world units
        float thresholdPosition = transform.position.y + cameraHeight * followThreshold;

        // If the Doodler is above the threshold, start following
        if (doodler.position.y > thresholdPosition && !player.isDead)
        {
            // Calculate the desired position with smooth damping
            Vector3 desiredPosition = new Vector3(transform.position.x, doodler.position.y - cameraHeight * followThreshold, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.5f);
            transform.position = smoothedPosition;
        }
    }
}