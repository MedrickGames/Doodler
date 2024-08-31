using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterS : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 15f;

    public PlayerScript player;

    void Start()
    {
        player = GameObject.Find("Doodler").GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0 || other.relativeVelocity.y > 0 && other.relativeVelocity.y < 1 && other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb =  other.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0,jumpSpeed);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("you died");
            player.die();
        }
    }
}
