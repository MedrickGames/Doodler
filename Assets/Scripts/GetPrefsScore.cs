using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;

public class GetPrefsScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField] 
    private TextMeshProUGUI highScore;

    private string username;

    private int iScore;

    private int iHighscore;
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        score.text = PlayerPrefs.GetInt("ThisGame",0).ToString();
        
        iScore = PlayerPrefs.GetInt("ThisGame");
        iHighscore = PlayerPrefs.GetInt("HighScore");

        username = PlayerPrefs.GetString("Username");
        
        if (iScore == iHighscore)
        {
            Debug.Log("This is username:"+username+"");
            UploadEntry();
        }
    }
    
    public void UploadEntry()
    {
        Leaderboards.Doodle.UploadNewEntry(PlayerPrefs.GetString("Username"), PlayerPrefs.GetInt("HighScore"));
    }


}
