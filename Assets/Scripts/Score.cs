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

    private void OnEnable()
    {
        SwordController.OnFirstClickEvent += StartAddScore;
        SwordController.OnLastClickEvent += StopAddScore;

        GameManager.OnSetPlayStateEvent += EnableVisibleScoreText;
        GameManager.OnSetMainMenuStateEvent += DisableVisibleScoreText;
        GameManager.OnSetSettingsStateEvent += DisableVisibleScoreText;
        GameManager.OnSetCollectionStateEvent += DisableVisibleScoreText;
    }

    private void Start()
    {
        _textScore = GetComponent<TMP_Text>();
    }

    private void OnDisable()
    {
        SwordController.OnFirstClickEvent -= StartAddScore;
        SwordController.OnLastClickEvent -= StopAddScore;

        GameManager.OnSetPlayStateEvent -= EnableVisibleScoreText;
        GameManager.OnSetMainMenuStateEvent -= DisableVisibleScoreText;
        GameManager.OnSetSettingsStateEvent -= DisableVisibleScoreText;
        GameManager.OnSetCollectionStateEvent -= DisableVisibleScoreText;
    }

    private void EnableVisibleScoreText()
    {
        _textScore.enabled = true;
    }

    private void DisableVisibleScoreText()
    {
        _textScore.enabled = false;
    }

    private void StartAddScore()
    {
        StartCoroutine("IAddScore");
    }

    private void StopAddScore()
    {
        StopCoroutine("IAddScore");
        OnGetScoreEvent?.Invoke(_score);
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
