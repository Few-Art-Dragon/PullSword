using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButton : MonoBehaviour, IButton
{
    private Image _imageButton;
    [SerializeField]
    private Sprite _buttonIsActive;
    [SerializeField]
    private Sprite _buttonIsNotActive;

    public event IButton.ClickHandler OnClickEvent;

    public void OnClick()
    {
        OnClickEvent?.Invoke(this);
    }

    public void SetSpriteActiveButton()
    {
        _imageButton.sprite = _buttonIsActive;
    }

    public void SetSpriteNotActiveButton()
    {
        _imageButton.sprite = _buttonIsNotActive;
    }

    private void Start()
    {
        _imageButton = GetComponent<Image>();
    }
}
