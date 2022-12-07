using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreSword : MonoBehaviour
{
    public int highScore { get; private set; }

    private void OnEnable()
    {
        Score.OnGetScoreEvent += CheckScoreOnHighScore;
    }

    private void OnDisable()
    {
        Score.OnGetScoreEvent -= CheckScoreOnHighScore;
    }

    private void CheckScoreOnHighScore(int score)
    {
        if (score>highScore)
        {
            SetHighScore(score);
        }
    }

    private void SetHighScore(int score)
    {
        highScore = score;
    }
}
