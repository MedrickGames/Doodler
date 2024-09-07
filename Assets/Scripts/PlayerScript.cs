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
    public bool isDead;
    public bool isPaused;
    private bool isOnCooldown = false; // Cooldown flag
    public float cooldownDuration = 0f;
    public AudioClip pew;
    public AudioSource playerAudio;
    public GameObject boogerHolder;
    void Start()
    {
        _cam = Camera.main;
        player = GetComponent<Rigidbody2D>();
        playerSprite = player.transform.GetComponent<SpriteRenderer>();
        screenWidth = Camera.main.aspect * Camera.main.orthographicSize + _cameraOffset;
    }
    
    void Update()
    {
        if (!isPaused)
        {
            CalculateMovement();
        }
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
        if (Input.GetMouseButtonDown(0) && !isDead && !isOnCooldown)
        {
            playerAudio.PlayOneShot(pew);
            var mousePosWorld = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cam.nearClipPlane));
            Vector3 aimDirection = mousePosWorld - transform.position;
            aimDirection.z = 0f;

            nose.SetActive(true);
            nose.transform.up = aimDirection.normalized;

            Booger newBooger = Instantiate(booger, spawnPoint.position, Quaternion.identity);
            newBooger.Init(nose.transform.up);
            
            newBooger.transform.parent = boogerHolder.transform;
            
            StartCoroutine(DestroyBoogerAfterTime(newBooger, 5f));StartShooting();

            StartCoroutine(ShootingCooldown());
            // Start the cooldown
        }
        
        IEnumerator DestroyBoogerAfterTime(Booger booger, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(booger.gameObject);
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

   IEnumerator ShootingCooldown()
   {
       isOnCooldown = true; // Set cooldown flag to true
       yield return new WaitForSeconds(0.2f); // Wait for the cooldown duration
       isOnCooldown = false; // Reset cooldown flag
   }
   IEnumerator ActiveShooter()
   {
       if (isShooting)
           yield break;

       isShooting = true;
       playerSprite.sprite = skins[4]; // Example: Set shooting skin
       nose.SetActive(true);

       yield return new WaitForSeconds(1.5f); // Wait for 1.5 seconds

       if (!isDead)
       {
           playerSprite.sprite = skins[2]; // Revert to another skin after shooting
       }
       nose.SetActive(false);
       isShooting = false;
   }

   void StartShooting()
   {
       if (activeShooterCoroutine != null)
       {
           StopCoroutine(activeShooterCoroutine);
           playerSprite.sprite = skins[2];
           nose.SetActive(false);
           isShooting = false;
       }

       activeShooterCoroutine = StartCoroutine(ActiveShooter());
   }


   public void die()
   {
       GameObject.Find("Lava").GetComponent<AudioManager>().PlayRandomAudio();
       isDead = true;
       nose.SetActive(false);
       playerSprite.sprite = skins[5];
       GetComponent<Rigidbody2D>().simulated = false;
       GetComponent<Rigidbody2D>().gravityScale = 0;
       GetComponent<BoxCollider2D>().isTrigger = true;
   }
}
