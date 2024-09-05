using System;
using Unity.VisualScripting;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public PlayerScript player;
     void Start()
     {
         player = GameObject.Find("Doodler").GetComponent<PlayerScript>();
     }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("You Died");
            player.die();
        }

        else if (other.transform.tag == "Platform")
        {
            Debug.Log("Plat Del");
            Destroy(other.gameObject);
        }

        else if (other.transform.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
