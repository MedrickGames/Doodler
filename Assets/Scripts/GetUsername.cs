using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetUsername : MonoBehaviour
{
    public TMP_InputField username;
    public GameObject UsernamePanel;
    public TextMeshProUGUI info;
    
    
    void Update()
    {
        
    }

    public void ValidateInput()
    {
        if (username.text != "")
        {
            PlayerPrefs.SetString("Username",username.text);
            UsernamePanel.SetActive(false);
        }
        else
        {
            info.color = Color.red;
            info.text = "Username Cant be empty!";
            Debug.Log("Username Cant be empty!");
        }

        
    }
}
