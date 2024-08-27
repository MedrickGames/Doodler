using System;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int score = 0;
    private float _tempScore;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI HighScoreUI;
    private int _highScore;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreUI.text = "" + _highScore;
    }

    void Update()
    {
        GetScore();
    }

    void GetScore()
    {
        _tempScore = player.transform.position.y;
        if (_tempScore > score)
        {
            score = (int)_tempScore;
            scoreUI.text = ""+score;
        }

        if (score > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            HighScoreUI.text = "" + score;
        }
    }
}
