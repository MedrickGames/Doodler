using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakingPlat : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0 || other.relativeVelocity.y > 0 && other.relativeVelocity.y < 1 && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Im on breaking");
            right.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            left.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            right.GetComponent<BoxCollider2D>().isTrigger = false;
            left.GetComponent<BoxCollider2D>().isTrigger = false;
            right.GetComponent<Rigidbody2D>().gravityScale = 1;
            left.GetComponent<Rigidbody2D>().gravityScale = 1;
            this.GetComponent<EdgeCollider2D>().enabled = false;
            this.GetComponent<PlatformEffector2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        
    }
    
    
}
