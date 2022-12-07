using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private List<MainButton> _buttons;

    [SerializeField]
    private IButton _currentActiveButton;

    private void GetCurrentActiveButton(IButton button)
    {
        _currentActiveButton = button;
        SetAllSpriteNotActiveButton();
        SetSpriteCurrentActiveButton();
    }

    private void SetAllSpriteNotActiveButton()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetSpriteNotActiveButton();
        }
    }

    private void SetSpriteCurrentActiveButton()
    {
        _currentActiveButton.SetSpriteActiveButton();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].OnClickEvent += GetCurrentActiveButton;
            Debug.Log(_buttons[i]);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].OnClickEvent -= GetCurrentActiveButton;
        }
    }
}
