using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
   public Sprite used;
   
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.relativeVelocity.y <= 0 || other.relativeVelocity.y > 0 && other.relativeVelocity.y < 1 && other.gameObject.CompareTag("Player"))
      {
         Rigidbody2D rb =  other.transform.GetComponent<Rigidbody2D>();
         rb.velocity = new Vector2(0,38);
         this.GetComponent<SpriteRenderer>().sprite = used;
      }
   }
}
