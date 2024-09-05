using System;
using UnityEngine.SceneManagement;
using System.Collections;
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
    private int _thisGameScore;
    private PlayerScript doodler;
    private bool showDead;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        PlayerPrefs.SetInt("ThisGame", 0);
        HighScoreUI.text = "" + _highScore;
        doodler = GameObject.Find("Doodler").GetComponent<PlayerScript>();
    }

    void Update()
    {
        GetScore();
        if (doodler.isDead && !showDead)
        {
            showDead = true;
            StartCoroutine(ShowDeathMen());
        }
    }

    void GetScore()
    {
        _tempScore = player.transform.position.y;
        if (_tempScore > score && !doodler.isDead)
        {
            score = (int)_tempScore;
            scoreUI.text = ""+score;
            PlayerPrefs.SetInt("ThisGame", score);
        }

        if (score > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            HighScoreUI.text = "" + score;
        }
    }

    IEnumerator  ShowDeathMen()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(3);
    }
}
