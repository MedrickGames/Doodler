using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 15f;
    private PlayerScript player;
    private Rigidbody2D platfrom;

    private void Start()
    {
        player = GameObject.Find("Doodler").GetComponent<PlayerScript>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0 || other.relativeVelocity.y > 0 && other.relativeVelocity.y < 1 && other.gameObject.CompareTag("Player"))
        {
            if (!player.isShooting)
            {
                StartCoroutine("JumpAn");
            }
            platformAnim();
            Rigidbody2D rb =  other.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0,jumpSpeed);
        }
    }
    
    void platformAnim()
    {
        
        transform.GetComponent<Animator>().SetTrigger("OnColi");
        
    }

    IEnumerator JumpAn()
    {
        while (GameObject.Find("Doodler").GetComponent<SpriteRenderer>().sprite == player.skins[2])
        {
            Debug.Log("jump start");
            GameObject.Find("Doodler").GetComponent<SpriteRenderer>().sprite = player.skins[3];
            yield return new WaitForSeconds(0.65f);
        }

        if (!player.isShooting && !player.isDead)
        {
            GameObject.Find("Doodler").GetComponent<SpriteRenderer>().sprite = player.skins[2];
        }
    }
}