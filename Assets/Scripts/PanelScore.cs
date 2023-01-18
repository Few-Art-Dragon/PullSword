using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelScore : MonoBehaviour
{
    private TMP_Text _scoreText;

    private void OnEnable()
    {
        SpawnSwords.OnGetScoreEvent += SetScoreText;
    }
    private void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
    }

    private void OnDisable()
    {
        SpawnSwords.OnGetScoreEvent -= SetScoreText;
    }

    private void SetScoreText(int num)
    {
        Debug.Log(num.ToString());
        _scoreText.SetText(num.ToString());
    }
}
