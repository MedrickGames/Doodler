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
    private Camera _cam;
    public Booger booger;
    public Transform spawnPoint;
    public bool isShooting;
    private bool hasChangedSprite;
    Coroutine activeShooterCoroutine;
    public bool isDead = false;
    
    void Start()
    {
        _cam = Camera.main;
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
        if (isDead)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y + 2.5f * Time.deltaTime, transform.position.z);
        }
    }

    void DidShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosWorld = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane));
        
            
            Vector3 aimDirection = mousePosWorld - transform.position;
            aimDirection.z = 0f;
            
            nose.SetActive(true);
            
            nose.transform.up = aimDirection.normalized;
    
            Instantiate(booger, spawnPoint.position, Quaternion.identity).Init(nose.transform.up);
            
            StartShooting();
            
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
       if (isShooting) // If already shooting, return
           yield break;

       isShooting = true; // Mark shooting as active

       // Perform any initial actions needed for shooting
       playerSprite.sprite = skins[4]; // Example: Set shooting skin
       nose.SetActive(true); // Example: Activate the nose

       yield return new WaitForSeconds(1.5f); // Wait for 5 seconds

       // Actions after shooting duration ends
       playerSprite.sprite = skins[2]; // Revert to another skin after shooting
       nose.SetActive(false); // Deactivate the nose
       isShooting = false; // Reset shooting state
   }
   void StartShooting()
   {
       if (activeShooterCoroutine != null)
       {
           // Stop the existing coroutine if one is running
           StopCoroutine(activeShooterCoroutine);

           // Ensure that the shooting state and skin are reset properly
           playerSprite.sprite = skins[2];
           nose.SetActive(false);
           isShooting = false;
       }

       // Start a new coroutine
       activeShooterCoroutine = StartCoroutine(ActiveShooter());
   }

   public void die()
   {
       isDead = true;
       playerSprite.sprite = skins[5];
       GetComponent<Rigidbody2D>().simulated = false;
       GetComponent<Rigidbody2D>().gravityScale = 0;
       GetComponent<BoxCollider2D>().isTrigger = true;
   }
}
