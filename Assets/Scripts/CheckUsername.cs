using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUsername : MonoBehaviour
{
    public GameObject usernamePanel;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Username"))
        {
            usernamePanel.SetActive(true);
        }
    }
    
}
