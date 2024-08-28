using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D player;
    private SpriteRenderer playerSprite;
    public GameObject nose;
    public List<Sprite> skins = new List<Sprite>(4);
    public float speed = 15f;
    private float _direction;
    private float screenWidth;
    private float _cameraOffset = 0.25f;
    private bool _isFirstShoot;
    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerSprite = player.transform.GetComponent<SpriteRenderer>();
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize + _cameraOffset;
    }
    
    void Update()
    {
        CalculateMovement();
    }

   void FixedUpdate()
   {
       var velocity = player.velocity;
       velocity.x = _direction * speed;
       player.velocity = velocity ;
   }
    void CalculateMovement()
    {
        ChangeDirection();
        IsOut();
        DidShoot();
    }

    void DidShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ActiveShooter());
        }
    }
    void IsOut()
   {
       if (transform.position.x > screenWidth)
       {
           transform.position = new Vector3(-screenWidth, transform.position.y, transform.position.z);
       }
       else if (transform.position.x < -screenWidth)
       {
           transform.position = new Vector3(screenWidth, transform.position.y, transform.position.z);
       }
   }

   void ChangeDirection()
   {
       _direction = Input.GetAxis("Horizontal");
        
       if (_direction > 0)
       {
           playerSprite.flipX = true;
       }
       else if (_direction < 0)
       {
            
           playerSprite.flipX = false;
       }
   }

   IEnumerator ActiveShooter()
   {
       while (true)
       {
        nose.SetActive(true);
        if (_isFirstShoot)
        {
            Debug.Log("shoot");
            _isFirstShoot = false;
        }
        yield return new WaitForSeconds(5f);
        break;
       }
       nose.SetActive(false);
       _isFirstShoot = true;
   }
}
