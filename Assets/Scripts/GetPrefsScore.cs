using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPrefsScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField] 
    private TextMeshProUGUI highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        score.text = PlayerPrefs.GetInt("ThisGame",0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
