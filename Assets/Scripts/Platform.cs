using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 15f;

    private Rigidbody2D platfrom;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0 || other.relativeVelocity.y > 0 && other.relativeVelocity.y < 1 && other.gameObject.CompareTag("Player"))
        {
            platformAnim();
            Rigidbody2D rb =  other.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0,jumpSpeed);
        }
    }

    
  

    void platformAnim()
    {
        
        transform.GetComponent<Animator>().SetTrigger("OnColi");
        
    }
}