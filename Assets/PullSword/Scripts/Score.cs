using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Score : MonoBehaviour
{

    public delegate void HandlerGetScore(int score);
    public static event HandlerGetScore OnGetScoreEvent;
    private TMP_Text _textScore;
    private int _score = 0;

    private void StartAddScore()
    {
        StartCoroutine("IAddScore");
    }

    private void StopAddScore()
    {
        StopCoroutine("IAddScore");
        OnGetScoreEvent?.Invoke(_score);
    }

    private void OnEnable()
    {
        SwordController.OnFirstClickEvent += StartAddScore;
        SwordController.OnLastClickEvent += StopAddScore;
    }

    private void Start()
    {
        _textScore = GetComponent<TMP_Text>();
    }

    private void OnDisable()
    {
        SwordController.OnFirstClickEvent -= StartAddScore;
        SwordController.OnLastClickEvent -= StopAddScore;
    }

    private void UpdateScoreText()
    {
        _textScore.text = _score.ToString();
    }

    IEnumerator IAddScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _score++;
            UpdateScoreText();
            _textScore.fontSize += 30;
            yield return new WaitForSeconds(0.05f);
            _textScore.fontSize -= 30;
        }
    }
}
