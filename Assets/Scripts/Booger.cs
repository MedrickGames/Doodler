using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booger : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<MonsterS>().lives--;
            if (other.transform.GetComponent<MonsterS>().lives == 0)
            {
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
            else
            {
                other.transform.GetComponent<SpriteRenderer>().sprite =
                    other.transform.GetComponent<MonsterS>().damage;
            }
            
        }
    }
}
