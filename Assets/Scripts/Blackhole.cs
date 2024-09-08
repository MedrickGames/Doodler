using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Blackhole : MonoBehaviour
{ 
    public float rotationDuration = 1f;
    public float scaleDuration = 1f; 
    public float moveDuration = 1f; 
    public float scaleDownFactor = 0f; 
    public float dragSpeed = 1f; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var vector2 = collision.GetComponent<Rigidbody2D>().velocity;
            vector2.y = 0f;
            vector2.x = 0f;
            collision.GetComponent<Rigidbody2D>().velocity = vector2;
            collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.GetComponent<Rigidbody2D>().simulated = false;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            collision.GetComponent<PlayerScript>().isDead = true;
            
            Transform playerTransform = collision.transform;
            
            playerTransform.DORotate(new Vector3(0, 0, 360*2), rotationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental);
            
            playerTransform.DOScale(new Vector3(0, 0, 1), scaleDuration);
            playerTransform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, playerTransform.position.z), moveDuration);
        }
    }

    
}