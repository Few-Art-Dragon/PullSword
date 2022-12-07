using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDescription : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _textDescription;

    private void SetActiveTextDescription()
    {
        _textDescription.enabled = true;
        StartCoroutine("IPlayAnimation");
    }

    private void ChangeFontSizeTextDescription(int num)
    {
        _textDescription.fontSize -= num;
    }

    private void OnEnable()
    {
        SwordController.OnLastClickEvent += SetActiveTextDescription;
    }

    private void Start()
    {
        _textDescription = GetComponent<TMP_Text>();
    }

    private void OnDisable()
    {
        SwordController.OnLastClickEvent -= SetActiveTextDescription;
    }

    IEnumerator IPlayAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _textDescription.fontSize += 15;
            yield return new WaitForSeconds(0.05f);
            _textDescription.fontSize -= 15;
        }
    }

}
