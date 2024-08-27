using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public int score = 0;
    private float _tempScore;
    public TextMeshProUGUI scoreUI;
    
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
    }
}
