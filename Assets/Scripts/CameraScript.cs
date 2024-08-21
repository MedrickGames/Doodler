using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform doodler; // Reference to the Doodler's transform
    public float smoothSpeed = 0.125f; // Smooth speed for camera movement
    public float followThreshold = 0.1f; // Threshold to start following (one-third of the screen)

    private float cameraHeight;

    void Start()
    {
        // Calculate half the height of the camera in world units
        cameraHeight = Camera.main.orthographicSize;
    }

    void LateUpdate()
    {
        // Determine the threshold position in world units
        float thresholdPosition = transform.position.y + cameraHeight * followThreshold;

        // If the Doodler is above the threshold, start following
        if (doodler.position.y > thresholdPosition)
        {
            // Calculate the desired position with smooth damping
            Vector3 desiredPosition = new Vector3(transform.position.x, doodler.position.y - cameraHeight * followThreshold, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.5f);
            transform.position = smoothedPosition;
        }
    }
}