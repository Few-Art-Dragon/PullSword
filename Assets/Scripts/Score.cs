using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Score : MonoBehaviour
{

    public static UnityEvent AddScoreEvent = new UnityEvent();
    public static UnityEvent<bool> FinishAddScoreEvent = new UnityEvent<bool>();

    [SerializeField] private TMP_Text _textScore;
    private int _score = 0;


    private void Start()
    {
        SetStandartParam();
        
    }

    private void SetStandartParam()
    {
        AddScoreEvent.AddListener(StartAddScore);
        FinishAddScoreEvent.AddListener(StopAddScore);
        _textScore = GetComponent<TMP_Text>();
    }

    private void StartAddScore()
    {
        StartCoroutine("IAddScore");
    }

    private void StopAddScore(bool isFinish)
    {
        if (isFinish)
        {
            Sword.SetHighScoreEvent.Invoke(_score);
        }
        else 
        {
            _score = 0;
        }

        
        _textScore.text = _score.ToString();
        StopCoroutine("IAddScore");
        
    }

    IEnumerator IAddScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _score++;
            _textScore.text = _score.ToString();
            _textScore.fontSize += 30;
            yield return new WaitForSeconds(0.05f);
            _textScore.fontSize -= 30;
        }
    }

    private void SwitchScoreText()
    {
        
    }
}
