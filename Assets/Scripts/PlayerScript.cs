using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D player;
    public float speed = 15f;
    private float _direction;
    private bool playerSprite;
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerSprite = player.transform.GetComponent<SpriteRenderer>().flipX;
    }
    
    void Update()
    {
        _direction = Input.GetAxis("Horizontal");
        
        if (_direction > 0)
        {
            player.transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_direction < 0)
        {
            
            player.transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }

   void FixedUpdate()
   {
       var velocity = player.velocity;
       velocity.x = _direction * speed;
       player.velocity = velocity ;
   }
}
