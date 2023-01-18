using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static MainMenu;

public class MainMenu : MonoBehaviour
{
    public delegate void GetGameStateHandler(GameState state);
    public static event GetGameStateHandler OnGetGameStateEvent;

    [SerializeField]
    private List<MainButton> _buttons;

    [SerializeField]
    private IButton _currentActiveButton;

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

    private void GetCurrentActiveButton(IButton button)
    {
        _currentActiveButton = button;
        OnGetGameStateEvent?.Invoke(_currentActiveButton.GetGameState());
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
}
