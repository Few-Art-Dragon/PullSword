using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class MainButton : MonoBehaviour, IButton
{
    private Image _imageButton;
    [SerializeField]
    private Sprite _buttonIsActive;
    [SerializeField]
    private Sprite _buttonIsNotActive;
    [SerializeField]
    private GameState _gameState;

    public event IButton.ClickHandler OnClickEvent;

    private void Start()
    {
        _imageButton = GetComponent<Image>();
    }

    

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

    public GameState GetGameState()
    {
        return _gameState;
    }
}
