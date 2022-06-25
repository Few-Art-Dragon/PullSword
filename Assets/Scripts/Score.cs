using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _textScore;
    private int _score = 0;

    public void StartAddScore()
    {
        StartCoroutine("IAddScore");
    }

    public void StopAddScore()
    {
        StopCoroutine("IAddScore");
    }

    IEnumerator IAddScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _score++;
            _textScore.text = _score.ToString();
        }
    }
}
