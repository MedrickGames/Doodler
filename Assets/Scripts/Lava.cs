using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int score;
    public int tempScore;
    public float time;
    public float tempTime;
    public Transform camera;

    void Start()
    {
        time = Time.time;
        score = GameObject.Find("GameManager").GetComponent<GameManager>().score;
        camera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        tempTime = Time.time;
        tempScore = GameObject.Find("GameManager").GetComponent<GameManager>().score;
        camera = GameObject.Find("Main Camera").transform;
        
        if (tempTime > time + 5f)
        {
            if (camera.position.y -2.6 > transform.position.y )
            {
                this.transform.position = new Vector3(transform.position.x,transform.position.y + 1 * Time.deltaTime,transform.position.z);
            }
        }

        if (tempScore > score)
        {
            time = Time.time;
            score = tempScore;
            transform.position = new Vector3(transform.position.x,camera.position.y-8.67f,transform.position.z);
        }
    }
}
