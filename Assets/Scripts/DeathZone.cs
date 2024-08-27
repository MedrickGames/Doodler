using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("You Died");
        }

        if (other.transform.tag == "Platform")
        {
            Debug.Log("Plat Del");
            Destroy(other.gameObject);
        }
    }
}
