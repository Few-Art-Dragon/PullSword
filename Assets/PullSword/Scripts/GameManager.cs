using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public delegate void SetPlayGameHandler();
    public static event SetPlayGameHandler OnSetPlayGameEvent;

    public static UnityEvent<IStateDay> OnChangeDayAndNightEvent = new UnityEvent<IStateDay>();

    [SerializeField]
    private SaveManager _saveManager;
    [SerializeField]
    private MainMusic _audioManager;

    [SerializeField]
    private Light _sun;

    [SerializeField]
    private GameObject _scoreGameObject;

    public static GameState gameState;

    private IStateDay stateDay;

    private void SetModeMainMenu()
    {
       // SetVisibleScore(false);
        gameState = GameState.MainMenu;
    }
    private void SetVisibleScore(bool active)
    {
        _scoreGameObject.SetActive(active);
    }

    private void SetVisableTextDescription(bool active)
    {
        _scoreGameObject.SetActive(active);
    }

    private void SetModeGame()
    {
        SetVisibleScore(true);
        gameState = GameState.Game;
    }

    private void SetModeGameOver()
    {
        gameState = GameState.GameOver;
    }

    private void SetModeCollection()
    {
        gameState = GameState.Collection;
    }

    private void SwitchDayAndNight(IStateDay stateDay)
    {
        this.stateDay = stateDay;
        this.stateDay.SetStateDay(_sun);
    }

    private void OnEnable()
    {
        SwordController.OnLastClickEvent += SetModeGameOver;

        OnChangeDayAndNightEvent.AddListener(SwitchDayAndNight);

    }
    private void Start()
    {
        SetModeMainMenu();
    }

    private void OnDisable()
    {
        SwordController.OnLastClickEvent += SetModeGameOver;

        OnChangeDayAndNightEvent.RemoveListener(SwitchDayAndNight);

    }

    public enum GameState : byte
    {
        MainMenu,
        Collection,
        Game,
        GameOver
    }
}
