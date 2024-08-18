using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 15f;

    private Rigidbody2D platfrom;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.y <= 0)
        {
            platformAnim();
            Rigidbody2D rb =  other.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0,jumpSpeed);
        }
    }

    void platformAnim()
    {
        platfrom = this.GetComponent<Rigidbody2D>();
        GetComponent<Animator>().parameters.SetValue();
    }
}